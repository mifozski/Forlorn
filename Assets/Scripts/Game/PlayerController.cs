using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityStandardAssets.Characters.FirstPerson;

using Forlorn;

namespace Forlorn
{
	public class PlayerController : MonoBehaviour
	{
		Redux.Store _store;
		public Redux.Store store {
			set
			{
				_store = value;
				_store.subscribe(OnChangeState);
			}
			get
			{
				return _store;
			}
		}

		new Transform camera;

		[SerializeField] float ineractiveDistance = 0.3f;

		[SerializeField] LayerMask interactableMask;

		// [SerializeField] UnityEvent interactableEvent;

		[SerializeField] InteractiveObjectTypeEvent interactiveEvent;

		private FirstPersonController firstPersonController;

		// Store stuff
		Reducers.GeneralState prevGeneralState = null;
		Reducers.SceneState prevSceneState = null;

		void Awake()
		{
			firstPersonController = gameObject.GetComponent<FirstPersonController>();
		}

		// Use this for initialization
		void Start()
		{
			prevGeneralState = store.getStateTree()[Reducers.general] as Reducers.GeneralState;
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
				if (interactive)
				{
					GameController.ShowInteractableObjectIndicator(true);
				}
			}
			else
			{
				GameController.ShowInteractableObjectIndicator(false);
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance + 1, interactableMask))
				{
					hit.transform.gameObject.SendMessage("OnInteracted");
					// if (interactive)
					// {
					// 	interactive.OnInteracted();
						// interactiveEvent.Invoke(interactive.interactiveType);
					// }
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
			return prevGeneralState.mainMenuEntered;
		}

		void OnChangeState(Redux.Store store)
		{
			Redux.StateTree state = store.getStateTree();

			var generalState = state[Reducers.general] as Reducers.GeneralState;
			if (prevGeneralState != generalState)
			{
				prevGeneralState = generalState;
			}

			var sceneState = state[Reducers.scene] as Reducers.SceneState;
			if (prevSceneState != sceneState)
			{
				gameObject.SetActive(true);

				prevSceneState = sceneState;
			}
		}
	}
}