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
	new Transform camera;

	[SerializeField] float ineractiveDistance = 0.3f;

	[SerializeField] LayerMask interactableMask;

	// [SerializeField] UnityEvent interactableEvent;

	[SerializeField] InteractiveObjectTypeEvent interactiveEvent;

	private FirstPersonController firstPersonController;

	public bool isPaused = false;

	void Awake()
	{
		firstPersonController = gameObject.GetComponent<FirstPersonController>();
	}

	// Use this for initialization
	void Start()
	{
		camera = GetComponentInChildren<CinemachineVirtualCamera>().transform;

		Debug.Log($"Setting pos: ${GameState.current.playerPosition.ToString()}");
		Debug.Log($"Setting rotation: ${GameState.current.playerOrientation.ToString()}");

		transform.position = GameState.current.playerPosition;
		Debug.Log("player rot: " + Mathf.Rad2Deg * GameState.current.playerOrientation.y);
		Debug.Log("camera rot: " + Mathf.Rad2Deg * GameState.current.playerOrientation.x);
		// transform.localRotation = Quaternion.Euler(0f, Mathf.Rad2Deg * GameState.current.playerOrientation.y, 0f);
		// camera.localRotation = Quaternion.Euler(Mathf.Rad2Deg * GameState.current.playerOrientation.x, 0f, 0f);
		gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot =
			Quaternion.Euler(0f, Mathf.Rad2Deg * GameState.current.playerOrientation.y, 0f);
		Debug.Log("m_CharacterTargetRot: " + gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot.ToString());
		Debug.Log("y: " + Mathf.Rad2Deg *  gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot.y + "deg");
		gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.m_CharacterTargetRot =
			Quaternion.Euler(Mathf.Rad2Deg * GameState.current.playerOrientation.x, 0f, 0f);
	}

	void Update()
	{
		firstPersonController.enabled = !isPaused;

		if (isPaused)
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
		GameState.current.playerOrientation = new Vector3(camera.rotation.x, transform.rotation.y, 0f);
	}

	void OnDrawGizmos()
	{
		if (camera)
		{
			Vector3 sight = camera.position + camera.forward * ineractiveDistance;
			Gizmos.DrawLine(camera.position, sight);
		}
	}
}
}