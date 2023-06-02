using System;
using UnityEngine;
using UnityEngine.Events;

namespace Forlorn
{
	public enum InteractiveObjectType
	{
		DoorMonitor,
		RoomDoor,
		NoteUnderTheDoor,
		Notebook
	}

	interface IInteractable
	{
		void OnInteracted();
	}

	public class InteractiveMixin : MonoBehaviour
	{
		// public InteractiveObjectType interactiveType;
		[SerializeField] float timeToActivate = 0.0f;

		public string onHoverSubtitles;

		[SerializeField] UnityEvent _onInteracted;

		public Action onInteracted;
		public Action onInteractedThisFrame;

		void Awake()
		{
			interactables = GetComponentsInChildren<IInteractable>();
		}

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
			if (activated)
			{
				return;
			}

			pressedTime += Time.deltaTime;

			// GameController.ShowPressToActivateProgressBar(true, );

			if (pressedTime > timeToActivate)
			{
				activated = true;

				foreach (var interactable in interactables)
				{
					interactable.OnInteracted();

				}
				onInteracted?.Invoke();

				_onInteracted?.Invoke();
			}
		}

		virtual public void OnInteractedThisFrame()
		{
			onInteractedThisFrame?.Invoke();
		}

		private float pressedTime;
		private bool activated = false;

		IInteractable[] interactables;
	}
}