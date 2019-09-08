using UnityEngine;
using UnityEngine.UI;

namespace Forlorn
{
	public class ScreenController : SingletonMonoBehavior<ScreenController>
	{
		[SerializeField] TMPro.TextMeshProUGUI subtitlesText;
		Coroutine hoverSubtitlesFadeOutCourutine = null;
		Coroutine subtitlesFadeOutCourutine = null;

		private string subtitles = "";

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
	}
}