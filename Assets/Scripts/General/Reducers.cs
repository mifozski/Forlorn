using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	static public class Reducers
	{
		public class SceneState
		{
			public List<int> loadedScenes = new List<int>();
		}

		public static Redux.Reducer scene = (object _state, object _action) => {
			var action = _action as Redux.Action;
			if (action.isInitialAction)
			{
				return new SceneState();
			}
			switch (action.type)
			{
			case "LOAD_SCENE":
			{
				int sceneId = action.to<int>();
				break;
			}
			case "SET_SCENE_LOADED":
			{
				int sceneId = (int)action.data?.GetType().GetProperty("id")?.GetValue(action.data, null);
				bool loaded = (bool)action.data?.GetType().GetProperty("loaded")?.GetValue(action.data, null);
				Utils.Log($"SCENE_LOADED: sceneId: {sceneId} loaded: {loaded}");
				SceneState state = _state as SceneState;
				if (loaded)
					state.loadedScenes.Add(sceneId);
				else
					state.loadedScenes.Remove(sceneId);
				return state;
			}
			}
			return _state;
		};

		public class GeneralState
		{
			public bool mainMenuEntered = false;
		}

		public static Redux.Reducer general = (object _state, object _action) => {
			var action = _action as Redux.Action;
			if (action.isInitialAction)
			{
				return new GeneralState();
			}
			if (action.type == "ENTER_MAIN_MENU")
			{
				GeneralState state = _state as GeneralState;

				state.mainMenuEntered = action.to<bool>();
				return state;
			}
			return _state;
		};
	}
}