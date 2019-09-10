using UnityEngine;

using Forlorn.Events;

namespace Forlorn
{
	[RequireComponent(typeof(Collider))]
	public class Interactable : MonoBehaviour
	{
		[SerializeField] string triggerId;

		public void Interact()
		{
			StringEventManager.Instance.TriggerEvent("invokeTrigger", triggerId);
		}
	}
}
