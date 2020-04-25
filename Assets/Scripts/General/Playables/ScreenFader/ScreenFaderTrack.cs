using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Forlorn.Playables
{
	[TrackColor(140f / 255f, 158f / 255f, 244f / 255f)]
	[TrackClipType(typeof(ScreenFaderClip))]
	public class ScreenFaderTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<ScreenFaderBehaviour> playable =
				ScriptPlayable<ScreenFaderBehaviour>.Create(graph, inputCount);

			return playable;
		}
	}
}
