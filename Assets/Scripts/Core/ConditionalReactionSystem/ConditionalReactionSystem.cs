
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Forlorn.Events;
using Forlorn.Core.Variables;

using Serialization;

namespace Forlorn.Core.ConditionSystem
{
	struct Trigger
	{
		public string condition;
		public string text;
		public string[] reactions;
		public string cues;
	}

	class ConditionalReactionSystem : MonoBehaviour
	{
		[SerializeField] private VariableCollection variableCollection;

		private Dictionary<string, List<Trigger>> reactionMapByReactionId = new Dictionary<string, List<Trigger>>();

		private ExpressionEvaluator evaluator = new ExpressionEvaluator();

		[ContextMenu("Re init")]
		void Start()
		{
			if (variableCollection == null)
			{
				Debug.LogError("No variable collection assigned to ConditionalReactionSystem");
				return;
			}

			GameState.current.variables = variableCollection.Variables;

			evaluator.Init(variableCollection.Variables);
			evaluator.HandleSetVariable = OnSetVariable;
			evaluator.HandleCallReaction = OnCallReaction;

			StringEventManager.Instance.StartListening("invokeTrigger", OnTrigger);

			List<TriggerData> triggerData;
			TriggerDataReader.Read(out triggerData);

			foreach (TriggerData data in triggerData)
			{
				if (reactionMapByReactionId.ContainsKey(data.id) == false)
				{
					reactionMapByReactionId.Add(data.id, new List<Trigger>());
				}

				var reactions = data.reaction.Split(';').Select(r => r.Replace("\"\"", "\"").TrimStart('"').TrimEnd('"')).Where(r => r.Length > 0).ToArray();

				reactionMapByReactionId[data.id].Add(new Trigger
				{
					condition = data.condition,
					text = data.text,
					reactions = reactions,
					cues = data.cues,
				});
			}
		}

		void OnTrigger(string triggerId)
		{
			List<Trigger> triggers;
			reactionMapByReactionId.TryGetValue(triggerId, out triggers);
			if (triggers != null)
			{
				Debug.Log($"Found {triggers.ToArray().Length} trigger(s) with ID {triggerId}");
				foreach (Trigger trigger in triggers)
				{
					if (evaluator.Evaluate(trigger.condition))
					{
						if (trigger.text.Length != 0)
						{
							ScreenController.Instance.ShowSubtitles(trigger.text);
						}

						foreach (string reaction in trigger.reactions)
						{
							evaluator.Evaluate(reaction);
							Debug.Log($"Triggered {reaction}");
						}
						break;
					}
				}
			}
			else
			{
				Debug.LogWarning($"NO TRIGGERS FOUND WITH ID {triggerId}");
			}
		}

		void OnCallReaction(string reaction)
		{
			StringEventManager.Instance.TriggerEvent("invokeReaction", reaction);
		}

		void OnSetVariable(string name, int value)
		{
			variableCollection.SetVariable(name, value);

			GameState.current.variables = variableCollection.Variables;
		}
	}
}