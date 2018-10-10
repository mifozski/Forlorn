public partial class SceneManagement
{
    public static class ActionCreators
    {
        public static Redux.ActionCreators<int> loadScene = (sceneId) => {
            return new Redux.Action {
                type = "CHANGE_SCENE",
                data = new {
                    id: sceneId
                }
            };
        };
    };
};