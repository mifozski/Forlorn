using UnityEngine;
using System.Collections.Generic;

using Cradle;
using Serialization;

using Forlorn.Core.Variables;

namespace Forlorn
{
	[System.Serializable]
	public class GameState
	{
		private static GameState _current;

		public static GameState current;

		public Dictionary<string, bool> StoryVars_Boolean { get; }

		public GameStage stage;
		public int sceneId = 0;

		public List<string> cutscenesPlayed = new List<string>();

		public List<Variable> variables = new List<Variable>();

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

		public void ReconcileStates(GameState other)
		{

		}
	}

	public static class ImmediateGameState
	{
		public static bool isInCutscene = false;
		public static bool isInMainMenu = false;
	}
}