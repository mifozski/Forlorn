#pragma warning disable 649

using UnityEngine;

using Forlorn.Events;

namespace Forlorn
{
	public class Interactable : MonoBehaviour
	{
		[SerializeField] string triggerId;

		public void Interact()
		{
			StringEventManager.Instance.TriggerEvent("invokeTrigger", triggerId);
		}
	}
}
