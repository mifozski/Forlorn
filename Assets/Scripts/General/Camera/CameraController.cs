using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	CameraPivotPoint [] pivots;

	Transform player;

	CameraPivotPoint currentPivot = null;

	Camera camera;

	private void Awake()
	{
		pivots = GameObject.FindObjectsOfType<CameraPivotPoint>();

		player = GameObject.FindObjectOfType<ThirdPersonPlayerController>().transform;

		camera = Camera.main;

		float w = 5.0f;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (currentPivot && currentPivot.IsPlayerInTriggerZone(player))
		{
			camera.transform.SetPositionAndRotation(currentPivot.GetPointTransfrom().position, currentPivot.GetPointTransfrom().rotation);
			return;
		}

		foreach (CameraPivotPoint pivot in pivots)
		{
			if (pivot.IsPlayerInTriggerZone(player))
			{
				camera.transform.SetPositionAndRotation(pivot.GetPointTransfrom().position, pivot.GetPointTransfrom().rotation);

				currentPivot = pivot;
				break;
			}
		}
	}
}
