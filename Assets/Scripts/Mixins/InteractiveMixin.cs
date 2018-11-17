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

	public string onHoverSubtitles;

	private void Reset()
	{
		gameObject.layer = LayerMask.NameToLayer("Interactive");
	}

	public void OnLookAt()
	{

	}

	public void OnHover(bool hover)
	{
		GameController.ShowInteractableObjectIndicator(hover);

		if (hover)
			GameController.ShowSubtitles(onHoverSubtitles);
		else
			GameController.ShowSubtitles("");
	}

	virtual public void OnInteracted()
	{
	}
}
}