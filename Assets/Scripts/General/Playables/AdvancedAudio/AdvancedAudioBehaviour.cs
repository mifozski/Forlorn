using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Forlorn.Playables
{
	[Serializable]
	public class AdvancedAudioBehaviour : PlayableBehaviour
	{
		AudioSource audioSource;

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			audioSource = playerData as AudioSource;

			if (audioSource == null)
			{
				return;
			}

			if (audioSource.isPlaying == false)
			{
				audioSource.Play();
			}
		}

		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (audioSource != null)
			{
				audioSource.Pause();
			}

			base.OnBehaviourPause(playable, info);
		}
	}
}