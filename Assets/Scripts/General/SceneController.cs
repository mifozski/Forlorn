using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	public class SceneController : MonoBehaviour
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

			if (BeforeSceneUnload != null)
				BeforeSceneUnload();

			yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

			yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

			if (AfterSceneLoad != null)
				AfterSceneLoad();

			yield return StartCoroutine(Fade(0f));
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