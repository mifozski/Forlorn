using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Forlorn
{
	public class SceneController : SingletonMonoBehavior<SceneController>
	{
		public event Action BeforeSceneUnload;
		public event Action AfterSceneLoad;
		// [SerializeField] Animator fadeInOutScreenAnimator;

		public CanvasGroup faderCanvasGroup;
		public float fadeDuration = 1f;

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

		public void LoadScene(int sceneId)
		{
			SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
		}

		public void FadeAndLoadScene(string sceneName)
		{
			if (!isFading)
			{
				StartCoroutine(FadeAndSwitchScenes(sceneName));
			}
		}

		private IEnumerator FadeAndSwitchScenes(string sceneName)
		{
			yield return StartCoroutine(Fade(1f));

			BeforeSceneUnload?.Invoke();

			for (int i = 1; i < SceneManager.sceneCount; i++)
			{
				yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
			}

			yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

			AfterSceneLoad?.Invoke();

			yield return StartCoroutine(Fade(0f));

			var startingPoint = GameObject.FindGameObjectWithTag("StartingPoint");
			if (startingPoint)
			{
				GameController.Instance.player.SetPositionAndRotation(startingPoint.transform.position, startingPoint.transform.rotation);
			}
		}

		private IEnumerator LoadSceneAndSetActive(string sceneName)
		{
			yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
			SceneManager.SetActiveScene(newlyLoadedScene);
		}

		private IEnumerator Fade(float finalAlpha)
		{
			isFading = true;
			faderCanvasGroup.blocksRaycasts = true;

			float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

			while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
			{
				faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
					fadeSpeed * Time.deltaTime);

				yield return null;
			}

			isFading = false;
			faderCanvasGroup.blocksRaycasts = false;
		}
	}
}
