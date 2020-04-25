using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Forlorn
{
	public class LevelController : SingletonMonoBehavior<SceneController>
	{
		public event Action BeforeSceneUnload;
		public event Action AfterSceneLoad;
		// [SerializeField] Animator fadeInOutScreenAnimator;

		public CanvasGroup faderCanvasGroup;
		public float fadeDuration = 1f;

		private int mainSceneId = 0;

		private bool isFading;

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

		public int[] GetLoadedSceneIds()
		{
			int[] sceneIds = new int[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				sceneIds[i] = SceneManager.GetSceneAt(i).buildIndex;
			}
			return sceneIds;
		}

		public void LoadLevel(string levelName)
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				if (i != 0)
				{
					SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
				}
			}

			SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
		}
	}
}
