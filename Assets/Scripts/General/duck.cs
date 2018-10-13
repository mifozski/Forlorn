namespace Forlorn
{
    public partial class SceneManagement
    {
        public static class ActionCreators
        {
            public static Redux.ActionCreator<int> loadScene = (sceneId) => {
                return new Redux.Action {
                    type = "LOAD_SCENE",
                    data = sceneId
                };
            };
        }
    }

    public partial class GameGeneral
    {
        public static class ActionCreators
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