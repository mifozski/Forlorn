#pragma warning disable 649

using UnityEngine;
using Cinemachine;
using UnityStandardAssets.Characters.FirstPerson;

using Forlorn.Events;

namespace Forlorn
{
	public class PlayerController : MonoBehaviour
	{
		new Transform camera;

		[SerializeField] float ineractiveDistance = 0.3f;

		[SerializeField] LayerMask interactableMask;

		// [SerializeField] UnityEvent interactableEvent;

		[SerializeField] InteractiveObjectTypeEvent interactiveEvent;

		private FirstPersonController firstPersonController;

		private InteractiveMixin prevHoveredObject = null;

		void Awake()
		{
			firstPersonController = gameObject.GetComponent<FirstPersonController>();
		}

		// Use this for initialization
		void Start()
		{
			// prevGeneralState = store.getStateTree()[Reducers.general] as Reducers.GeneralState;
			camera = GetComponentInChildren<CinemachineVirtualCamera>().transform;
		}

		void Update()
		{
			firstPersonController.enabled = !IsPaused();

			if (IsPaused())
			{
				return;
			}

			RaycastHit hit;
			if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance, interactableMask))
			{
				InteractiveMixin interactive = hit.transform.gameObject.GetComponent<InteractiveMixin>();
				if (prevHoveredObject && prevHoveredObject != interactive)
				{
					prevHoveredObject.SendMessage("OnHover", false);
				}

				if (interactive)
				{
					// GameController.ShowInteractableObjectIndicator(true);
					interactive.transform.gameObject.BroadcastMessage("OnHover", true);
				}

				prevHoveredObject = interactive;
			}
			else
			{
				// GameController.ShowInteractableObjectIndicator(false);
				if (prevHoveredObject)
					prevHoveredObject.SendMessage("OnHover", false);
				prevHoveredObject = null;
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance + 1, interactableMask))
				{
					hit.transform.gameObject.SendMessageUpwards("OnInteracted", SendMessageOptions.DontRequireReceiver);
					hit.transform.gameObject.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		void OnDrawGizmos()
		{
			if (camera)
			{
				Vector3 sight = camera.position + camera.forward * ineractiveDistance;
				Gizmos.DrawLine(camera.position, sight);
			}
		}

		bool IsPaused()
		{
			return ImmediateGameState.isInMainMenu || ImmediateGameState.isInCutscene;
		}
	}
}
