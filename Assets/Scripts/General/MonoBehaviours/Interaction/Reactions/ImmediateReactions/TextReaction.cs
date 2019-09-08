using UnityEngine;

namespace Forlorn
{
	public class TextReaction : Reaction
	{
		public string message;
		public Color textColor = Color.white;
		public float delay;

		// private TextManager textManager;


		protected override void SpecificInit()
		{
			// textManager = FindObjectOfType<TextManager>();
		}


		protected override void ImmediateReaction()
		{
			SubtitleController.Instance.ShowHoverSubtitles(message);
			// textManager.DisplayMessage(message, textColor, delay);
		}
	}
}