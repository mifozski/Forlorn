using System.Collections.Generic;
using UnityEngine;

using Cradle;
using Serialization;

namespace Story.Game
{
	[System.Serializable]
    public class GameState
    {
		public static GameState Current;

		public Dictionary<string, bool> StoryVars_Boolean { get; }

		public GameState()
		{
			StoryVars_Boolean = new Dictionary<string, bool>();
		}

		public void SetStoryVars(RuntimeVars storyVars)
		{
			foreach (KeyValuePair<string, StoryVar> storyVar in storyVars)
			{
				Debug.Log($"Setting '${storyVar.Key}: ${storyVar.Value}");
				if (storyVar.Value.InnerValue is bool)
				{
					StoryVars_Boolean[storyVar.Key] = storyVar.Value.ConvertValueTo<bool>();
				}
			}

			PersistenceController.Save();
		}

		public bool StoryVarIsSet(string varName)
		{
			bool val;
			StoryVars_Boolean.TryGetValue(varName, out val);
			return val;
		}

		public void SetStoryVar(string name, bool value)
		{
			Debug.Log($"Setting '${name}: ${value}");
			StoryVars_Boolean[name] = value;

			PersistenceController.Save();
		}
    }

	public static class ImmediateGameState
	{
		public static bool inCutscene = false;
	}
}