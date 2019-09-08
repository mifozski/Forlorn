using UnityEngine;
using UnityEngine.UI;

namespace Forlorn
{
	public class SubtitleController : SingletonMonoBehavior<SubtitleController>
	{
		[SerializeField] TMPro.TextMeshProUGUI subtitlesText;
		Coroutine subtitlesFadeOutCourutine = null;

		private string subtitles = "";

		void Start()
		{
		}

		public void ShowHoverSubtitles(string text)
		{
			if (subtitlesFadeOutCourutine != null)
				Instance.StopCoroutine(subtitlesFadeOutCourutine);

			subtitles = text;
			Instance.subtitlesText.text = text;

			subtitlesFadeOutCourutine = Instance.StartCoroutine(Utils.FadeOutText(subtitlesText, 6f, 0f));
		}

		public void ShowSubtitles(string text)
		{
			if (subtitlesFadeOutCourutine != null)
				Instance.StopCoroutine(subtitlesFadeOutCourutine);

			Instance.subtitlesText.text = text;

			subtitlesFadeOutCourutine = Instance.StartCoroutine(Utils.FadeOutText(subtitlesText, 6f, 0f));
		}
	}
}