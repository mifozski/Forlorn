using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Forlorn.Playables
{
	[Serializable]
	public class ScreenFaderClip : PlayableAsset, ITimelineClipAsset
	{
		[HideInInspector]
		public ScreenFaderBehaviour template = new ScreenFaderBehaviour();

		public ClipCaps clipCaps => ClipCaps.None;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<ScreenFaderBehaviour>.Create(graph, template);
			ScreenFaderBehaviour clone = playable.GetBehaviour();

			return playable;
		}
	}
}