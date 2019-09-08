using UnityEngine;

using Forlorn.Events;

namespace Forlorn
{
	[RequireComponent(typeof(Collider))]
	public class Interactable : MonoBehaviour
	{
		// public Transform interactionLocation;
		// public ConditionCollection[] conditionCollections = new ConditionCollection[0];
		// public ReactionCollection defaultReactionCollection;
		[SerializeField] string triggerId;

		public void Interact()
		{
			StringEventManager.Instance.TriggerEvent("invokeTrigger", triggerId);
			// for (int i = 0; i < conditionCollections.Length; i++)
			// {
			// 	if (conditionCollections[i].CheckAndReact())
			// 		return;
			// }

			// defaultReactionCollection.React();
		}
	}
}
