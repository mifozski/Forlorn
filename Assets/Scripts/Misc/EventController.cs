using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
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


	public class EventController<T> : SingletonMonoBehavior<EventController<T>>
	{
		private Dictionary <string, UnityEvent<T>> eventDictionary = new Dictionary<string, UnityEvent<T>>();

		private class EventImpl : UnityEvent<T> { } ;

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
}