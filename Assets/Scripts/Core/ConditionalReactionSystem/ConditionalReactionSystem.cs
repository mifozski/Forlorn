
using System.Collections.Generic;
using UnityEngine;

using Forlorn.Events;

namespace Forlorn.ConditionSystem
{
	struct Trigger
	{
		public string reaction;
		public string condition;
		public string cues;
	}

	class ConditionalReactionSystem : MonoBehaviour
	{
		private Dictionary<string, List<Trigger>> reactionMapByReactionId = new Dictionary<string, List<Trigger>>();

		[ContextMenu("Re init")]
		void Start()
		{
			StringEventManager.Instance.StartListening("invokeTrigger", OnTrigger);

			List<TriggerData> triggerData;
			TriggerDataReader.Read(out triggerData);

			foreach (TriggerData data in triggerData)
			{
				Debug.Log($"TEXT DATA WITH id {data.id} has been read");

				if (reactionMapByReactionId.ContainsKey(data.id) == false)
				{
					reactionMapByReactionId.Add(data.id, new List<Trigger>());
				}

				reactionMapByReactionId[data.id].Add(new Trigger { reaction = data.reaction, condition = data.condition, cues = data.cues });
			}
		}

		void OnTrigger(string triggerId)
		{
			List<Trigger> triggers;
			reactionMapByReactionId.TryGetValue(triggerId, out triggers);
			if (triggers != null)
			{
				Debug.Log($"Found {triggers.ToArray().Length} trigger(s) with ID {triggerId}");
				// Invoke first reaction first for test
				StringEventManager.Instance.TriggerEvent("invokeReaction", triggers[0].reaction);
				Debug.Log($"Triggered {triggers[0].reaction}");
			}
			else
			{
				Debug.LogWarning($"NO TRIGGERS FOUND WITH ID {triggerId}");
			}
		}
	}
}