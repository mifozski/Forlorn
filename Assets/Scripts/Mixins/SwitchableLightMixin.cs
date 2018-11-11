using UnityEngine;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(Light), typeof(AudioSource))]
	public class SwitchableLightMixin : MonoBehaviour
	{
		private new Light light;
		private AudioSource humming;

		Animator doorAnimator;

		void Awake()
		{
			light = GetComponentInChildren<Light>();

			humming = GetComponent<AudioSource>();
			humming.loop = true;
			PlayHumming(light.enabled);
		}

		public void ToggleLight()
		{
			light.enabled = !light.enabled;
			PlayHumming(!humming.isPlaying);
		}

		public bool IsOn()
		{
			return light.enabled;
		}

		private void PlayHumming(bool play)
		{
			if (play)
				humming.Play();
			else
				humming.Stop();
		}
	}
}