using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Forlorn.Playables
{
	[Serializable]
	public class AdvancedAudioClip : PlayableAsset, ITimelineClipAsset
	{
		[HideInInspector]
		public AdvancedAudioBehaviour template = new AdvancedAudioBehaviour();

		public ClipCaps clipCaps => ClipCaps.None;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<AdvancedAudioBehaviour>.Create(graph, template);
			AdvancedAudioBehaviour clone = playable.GetBehaviour();

			return playable;
		}
	}
}