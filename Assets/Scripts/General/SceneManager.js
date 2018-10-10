using UnityEngine;

static public class SceneManager
{
    [SerializeField] Animator fadeInOutScreenAnimator;

    public static LoadScene()
    {
        SceneManager.LoadScene(GetSceneName(scene));
    }
}