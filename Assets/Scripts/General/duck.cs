using UnityEngine;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
    public partial class ActionCreators
    {
        public static class SceneManagement
        {
            public static Redux.ActionCreator<int> loadScene = (sceneId) => {
                return new Redux.Action {
                    type = "LOAD_SCENE",
                    data = sceneId
                };
            };

            public static Redux.ActionCreator<int, bool> setSceneLoaded = (sceneId, loaded) => {
                return new Redux.Action {
                    type = "SET_SCENE_LOADED",
                    data = new {
                        id = sceneId,
                        loaded = loaded
                    }
                };
            };
        }

        public partial class GameGeneral
        {
            public static Redux.ActionCreator<bool> enterMainMenu = (entered) => {
                return new Redux.Action {
                    type = "ENTER_MAIN_MENU",
                    data = entered
                };
            };
        }
    }

    static public partial class Reducers
	{
		[System.Serializable]
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
				int sceneId = Utils.GetActionData<int>(action, "id");
				bool loaded = Utils.GetActionData<bool>(action, "loaded");
				SceneState state = _state as SceneState;
				if (loaded)
				{
					if (state.loadedScenes.Contains(sceneId))
					{
						Debug.LogError($"SCENE IS ALREADY LOADED: {sceneId}");
					}
					state.loadedScenes.Add(sceneId);
				}
				else
					state.loadedScenes.Remove(sceneId);
				return state;
			}
			}
			return _state;
		};

		[System.Serializable]
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
