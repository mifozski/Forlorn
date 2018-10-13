using UnityEngine;
using UnityEngine.SceneManagement;

using Forlorn;

namespace Forlorn
{
    static public class SceneManager
    {
        // [SerializeField] Animator fadeInOutScreenAnimator;

        // void Awake()
        // {

        // }

        public static void LoadScene(int sceneId)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
        }
    }
}