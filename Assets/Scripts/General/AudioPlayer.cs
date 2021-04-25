using UnityEngine;

namespace Forlorn
{
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] AudioSource _audioSource;
		[SerializeField] ReactionPlayerSO _so;

		void Awake()
		{
			_so.SetPlayer(this);
		}

		public void Play(AudioClip clip)
		{
			_audioSource.PlayOneShot(clip);
		}
	}
}
