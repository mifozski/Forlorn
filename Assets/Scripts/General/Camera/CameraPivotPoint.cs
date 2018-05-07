using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotPoint : MonoBehaviour {

	Transform point;

	BoxCollider triggerZone;

	private void Awake()
	{
		point = gameObject.transform.GetChild(0).transform;

		triggerZone = gameObject.GetComponentInChildren<BoxCollider>();
	}

	public Transform GetPointTransfrom()
	{
		return point;
	}

	public bool IsPlayerInTriggerZone(Transform player)
	{
		Vector3 w = player.position;

		return triggerZone.bounds.Contains(player.position);
	}
}
