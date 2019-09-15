#pragma warning disable 649

using System.Collections;
using UnityEngine;

namespace Forlorn
{
	public class ScreenController : SingletonMonoBehavior<ScreenController>
	{
		[SerializeField] TMPro.TextMeshProUGUI subtitlesText;
		Coroutine hoverSubtitlesFadeOutCourutine = null;
		Coroutine subtitlesFadeOutCourutine = null;

		public CanvasGroup faderCanvasGroup;

		private string subtitles = "";

		bool isFading = false;

		void Start()
		{
		}

		public void ShowHoverSubtitles(string text)
		{
			if (subtitlesFadeOutCourutine != null)
			{
				return;
			}

			if (hoverSubtitlesFadeOutCourutine != null)
				Instance.StopCoroutine(hoverSubtitlesFadeOutCourutine);

			subtitles = text;
			Instance.subtitlesText.text = text;

			hoverSubtitlesFadeOutCourutine = Instance.StartCoroutine(Utils.FadeOutText(subtitlesText, 6f, 0f, () => { hoverSubtitlesFadeOutCourutine = null; }));
		}

		public void ShowSubtitles(string text)
		{
			if (subtitlesFadeOutCourutine != null)
				Instance.StopCoroutine(subtitlesFadeOutCourutine);

			Instance.subtitlesText.text = text;

			subtitlesFadeOutCourutine = Instance.StartCoroutine(Utils.FadeOutText(subtitlesText, 6f, 0f, () => { subtitlesFadeOutCourutine = null; }));
		}

		public void FadeOutScreen(float duration)
		{
			if (!isFading)
			{
				StartCoroutine(Fade(1.0f, duration));
			}
		}

		public void FadeInScreen(float duration)
		{
			if (!isFading)
			{
				StartCoroutine(Fade(0.0f, duration));
			}
		}

		public void setScreenFade(float value)
		{
			faderCanvasGroup.alpha = value;
		}

		private IEnumerator Fade(float finalAlpha, float fadeDuration = 1f)
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