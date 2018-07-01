using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Forlorn;

namespace Forlorn
{
public enum InteractiveObjectType
{
	DoorMonitor,
	RoomDoor,
	NoteUnderTheDoor,
	Notebook
}

public class Interactive : MonoBehaviour
{
	public InteractiveObjectType interactiveType;

	private void Reset()
	{
		gameObject.layer = LayerMask.NameToLayer("Interactive");
	}

	public void OnLookAt()
	{
		
	}

	private void OnInteract()
	{
		// ue.Invoke();
	}
}
}