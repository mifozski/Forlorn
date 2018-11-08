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

public class InteractiveMixin : MonoBehaviour
{
	// public InteractiveObjectType interactiveType;

	private void Reset()
	{
		gameObject.layer = LayerMask.NameToLayer("Interactive");
	}

	public void OnLookAt()
	{

	}

	virtual public  void OnInteracted()
	{
		// ue.Invoke();
	}
}
}