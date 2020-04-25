using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Forlorn.Playables
{
	[Serializable]
	public class ScreenFaderBehaviour : PlayableBehaviour
	{
		public float fadeOutDuration;
		[SerializeField] float fadeInDuration;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (playable.GetInputCount() == 0)
			{
				return;
			}

			ScreenController.Instance.SetScreenFade(playable.GetInputWeight(0));
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			base.OnBehaviourPause(playable, info);
		}
	}
}
