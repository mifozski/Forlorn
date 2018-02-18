using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Forlorn;

namespace Forlorn
{
public class PlayerController : MonoBehaviour {

	Transform camera;

	[SerializeField]
	float ineractiveDistance = 0.3f;

	[SerializeField]
	LayerMask interactableMask;

	// Use this for initialization
	void Start ()
	{
		camera = GetComponentInChildren<Camera>().transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit hit;
			if (Physics.Raycast(camera.position, camera.forward, out hit, ineractiveDistance, interactableMask))
			{
				Debug.Log("OnUse_" + hit.transform.gameObject.name);
				EventManager.TriggerEvent("OnUse_" + hit.transform.gameObject.name);
			}
		}
	}
}
}