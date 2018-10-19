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
}
