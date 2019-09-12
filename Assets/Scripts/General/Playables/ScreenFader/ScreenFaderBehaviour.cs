using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Forlorn.Playables
{
	[Serializable]
	public class ScreenFaderBehaviour : PlayableBehaviour
	{
		public float fadeOutDuration;
		[SerializeField] float fadeInDuration;

		bool fadedOut = false;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (fadedOut == false)
			{

				ScreenController.Instance.FadeOutScreen(fadeOutDuration);
				fadedOut = true;
			}
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			base.OnBehaviourPause(playable, info);
		}
	}
}