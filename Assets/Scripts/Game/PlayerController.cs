using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Forlorn;

namespace Forlorn
{
public class PlayerController : MonoBehaviour {

	new Transform camera;

	[SerializeField]
	float ineractiveDistance = 0.3f;

	[SerializeField]
	LayerMask interactableMask;

	// [SerializeField] UnityEvent interactableEvent;

	[SerializeField] InteractiveObjectTypeEvent interactiveEvent;

	// Use this for initialization
	void Start()
	{
		camera = GetComponentInChildren<Camera>().transform;
	}
	
	// Update is called once per frame
	void Update()
	{
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
			if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance, interactableMask))
			{
				Interactive interactive = hit.transform.gameObject.GetComponentInChildren<Interactive>();
				if (interactive)
				{
					interactiveEvent.Invoke(interactive.interactiveType);
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
}
}