using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	public class CutsceneControllerState
		{
			public Dictionary<Cutscene, bool> finishedCutscenes = new Dictionary<Cutscene, bool>();
			public bool playing = false;
		}

	static public partial class ActionCreators
	{
		public static class Cutscenes
        {
            public static Redux.ActionCreator setCutsceneStarted = () => {
                return new Redux.Action {
                    type = "SET_CUTSCENE_STARTED"
                };
            };

            public static Redux.ActionCreator<Cutscene> setCutsceneFinished = (cutscene) => {
                return new Redux.Action {
                    type = "SET_CUTSCENE_FINISHED",
                    data = cutscene
                };
            };
        }
	}

	static public partial class Reducers
	{
		public static Redux.Reducer cutscenes = (object _state, object _action) => {
			var action = _action as Redux.Action;
			CutsceneControllerState state = _state as CutsceneControllerState;
			if (action.isInitialAction)
			{
				return new CutsceneControllerState();
			}
			switch (action.type)
			{
			case "SET_CUTSCENE_STARTED":
			{
				state.playing = true;
				return state;
			}
			case "SET_CUTSCENE_FINISHED":
			{
				state.playing = false;
				state.finishedCutscenes[action.to<Cutscene>()] = true;
				return state;
			}
			}
			return _state;
		};
	}
}