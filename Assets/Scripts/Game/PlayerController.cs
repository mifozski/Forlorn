#pragma warning disable 649

using UnityEngine;
using Cinemachine;
using System.Runtime.Serialization;
using UnityStandardAssets.Characters.FirstPerson;

using Serialization;

using Forlorn.Events;

namespace Forlorn
{
	public class PlayerController : MonoBehaviour, OnSerializeCallback
	{
		public static PlayerController player;

		new Transform camera;

		[SerializeField] float ineractiveDistance = 0.3f;

		[SerializeField] LayerMask interactableMask;

		[SerializeField] InteractiveObjectTypeEvent interactiveEvent;

		private FirstPersonController firstPersonController;

		private InteractiveMixin prevHoveredObject = null;

		void Awake()
		{
			player = this;

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
			if (firstPersonController.enabled != !IsPaused())
			{
				firstPersonController.enabled = !IsPaused();
			}

			if (IsPaused())
			{
				return;
			}

			if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, ineractiveDistance, interactableMask))
			{
				InteractiveMixin interactive = hit.transform.gameObject.GetComponentInParent<InteractiveMixin>();
				// Debug.LogError("interactive:" + interactive, hit.transform);
				if (prevHoveredObject && prevHoveredObject != interactive)
				{
					prevHoveredObject.SendMessage("OnHover", false, SendMessageOptions.DontRequireReceiver);
				}

				if (interactive)
				{
					// GameController.ShowInteractableObjectIndicator(true);
					interactive.transform.gameObject.BroadcastMessage("OnHover", true, SendMessageOptions.DontRequireReceiver);
				}

				prevHoveredObject = interactive;
			}
			else
			{
				// GameController.ShowInteractableObjectIndicator(false);
				if (prevHoveredObject)
				{
					prevHoveredObject.SendMessage("OnHover", false);
				}
				prevHoveredObject = null;
			}

			if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
			{
				if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance + 1, interactableMask))
				{
					var pressedThisFrame = Input.GetKeyDown(KeyCode.E);

					var mixin = hit.transform.GetComponentInParent<InteractiveMixin>();
					if (mixin)
					{
						Debug.LogError("mixin: " + mixin);
						if (pressedThisFrame)
						{
							// Debug.LogError("this frame");
							mixin.transform.root.SendMessage("OnInteractedThisFrame");
							mixin.OnInteractedThisFrame();
						}
						else
						{
							// Debug.LogError("not this frame");
							mixin.OnInteracted();
						}
					}
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

		public void TransferTo(Transform toTransform)
		{
			transform.SetPositionAndRotation(toTransform.position, toTransform.rotation);
		}

		public void OnSerialize(ref SerializationInfo info)
		{
			info.AddValue("cameraOrientation", firstPersonController.GetOrientation());
		}

		public void OnDeserialize(SerializationInfo info)
		{

			firstPersonController.SetOrientation((Quaternion)info.GetValue("cameraOrientation", typeof(Quaternion)));
		}
	}
}
