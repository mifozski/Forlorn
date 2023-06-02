#pragma warning disable 649

using UnityEngine;

using Forlorn.Events;

namespace Forlorn
{
	public class Interactable : MonoBehaviour, IInteractable
	{
		[SerializeField] string triggerId;

		public void OnInteracted()
		{
			StringEventManager.Instance.TriggerEvent("invokeTrigger", triggerId);
		}
	}
}
