using UnityEngine;
using UnityEngine.Timeline;

namespace Forlorn.Playables
{
	[TrackColor(140f / 255f, 158f / 255f, 244f / 255f)]
	[TrackBindingType(typeof(AudioSource))]
	[TrackClipType(typeof(AdvancedAudioClip))]
	public class AdvancedAudioTrack : TrackAsset
	{

	}
}