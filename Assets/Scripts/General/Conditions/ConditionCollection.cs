using UnityEngine;

namespace Forlorn
{
	[CreateAssetMenu]
	public class ConditionCollection : ScriptableObject
	{
		public string description;
		public Condition[] requiredConditions = new Condition[0];
		public ReactionCollection reactionCollection;
		public bool CheckAndReact()
		{
			Debug.Log($"CheckAndReact for {description}");
			for (int i = 0; i < requiredConditions.Length; i++)
			{
				if (!AllConditions.CheckCondition(requiredConditions[i]))
					return false;
			}
			if (reactionCollection)
			{
				reactionCollection.React();
				Debug.Log($"Pass for {description}!");
			}
			else
			{
				Debug.LogError($"Fail for {description}!");
			}
			return true;
		}
	}
}