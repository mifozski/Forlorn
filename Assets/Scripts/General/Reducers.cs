using Forlorn;

namespace Forlorn
{
    static public class Reducers
    {
        public static Redux.Reducer scene = (object state, object _action) => {
            var action = _action as Redux.Action;
            if (action.isInitialAction)
            {
                return 0;
            }
            if (action.type == "LOAD_SCENE")
            {
                int sceneId = action.to<int>();
                SceneManager.LoadScene(sceneId);
            }
            return state;
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