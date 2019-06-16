using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	public class SceneController : MonoBehaviour
	{
		// [SerializeField] Animator fadeInOutScreenAnimator;

		public void Awake()
		{
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			// store.dispatch(ActionCreators.SceneManagement.setSceneLoaded(scene.buildIndex, true));
		}

		void OnSceneUnloaded(Scene scene)
		{
			// store.dispatch(ActionCreators.SceneManagement.setSceneLoaded(scene.buildIndex, false));
		}

		public void LoadScene(int sceneId)
		{
			SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
		}
	}
}