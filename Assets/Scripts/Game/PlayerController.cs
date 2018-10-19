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
			camera = GetComponentInChildren<CinemachineVirtualCamera>().transform;

			// Debug.Log($"Setting pos: ${GameState.current.playerPosition.ToString()}");
			// Debug.Log($"Setting rotation: ${GameState.current.playerOrientation.ToString()}");

			// transform.position = GameState.current.playerPosition;
			// Debug.Log("player rot: " + Mathf.Rad2Deg * GameState.current.playerOrientation.y);
			// Debug.Log("camera rot: " + Mathf.Rad2Deg * GameState.current.playerOrientation.x);
			// // transform.localRotation = Quaternion.Euler(0f, Mathf.Rad2Deg * GameState.current.playerOrientation.y, 0f);
			// // camera.localRotation = Quaternion.Euler(Mathf.Rad2Deg * GameState.current.playerOrientation.x, 0f, 0f);
			// gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot =
			// 	Quaternion.Euler(0f, Mathf.Rad2Deg * GameState.current.playerOrientation.y, 0f);
			// Debug.Log("m_CharacterTargetRot: " + gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot.ToString());
			// Debug.Log("y: " + Mathf.Rad2Deg *  gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot.y + "deg");
			// gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot =
			// 	Quaternion.Euler(Mathf.Rad2Deg * GameState.current.playerOrientation.x, 0f, 0f);
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
				Interactive interactive = hit.transform.gameObject.GetComponent<Interactive>();
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
					Interactive interactive = hit.transform.gameObject.GetComponentInChildren<Interactive>();
					if (interactive)
					{
						interactiveEvent.Invoke(interactive.interactiveType);
					}
				}
			}

			GameState.current.playerPosition = transform.position;
			GameState.current.playerRotation = Quaternion.Euler(camera.rotation.x, transform.rotation.y, 0f);
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