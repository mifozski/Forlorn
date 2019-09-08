using UnityEngine;

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
				ScreenController.Instance.ShowHoverSubtitles(onHoverSubtitles);
			else
				ScreenController.Instance.ShowHoverSubtitles("");
		}

		virtual public void OnInteracted()
		{
		}
	}
}