using UnityEngine.Events;
using System.Collections.Generic;

namespace Forlorn.Events
{
	[System.Serializable]
	public class InteractiveObjectTypeEvent : UnityEvent<InteractiveObjectType>
	{
	}

	[System.Serializable]
	public class IntEvent : UnityEvent<int>
	{
	}

	[System.Serializable]
	public class FloatEvent : UnityEvent<float>
	{
	}

	[System.Serializable]
	public class StringEvent : UnityEvent<string>
	{
	}

	public class EventManager<T> : SingletonMonoBehavior<EventManager<T>>
	{
		private Dictionary<string, UnityEvent<T>> eventDictionary = new Dictionary<string, UnityEvent<T>>();

		private class EventImpl : UnityEvent<T> { };

		public void StartListening(string eventName, UnityAction<T> listener)
		{
			UnityEvent<T> thisEvent = null;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new EventImpl();

				thisEvent.AddListener(listener);
				eventDictionary.Add(eventName, thisEvent);
			}
		}

		public void StopListening(string eventName, UnityAction<T> listener)
		{
			UnityEvent<T> thisEvent = null;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.RemoveListener(listener);
			}
		}

		public void TriggerEvent(string eventName, T param)
		{
			UnityEvent<T> thisEvent = null;
			if (eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.Invoke(param);
			}
		}
	}

	// [System.Serializable]
	// public class StringEventManager : EventManager<string> { };
}