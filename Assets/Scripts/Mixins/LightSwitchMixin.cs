using UnityEngine;
using System.Linq;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider), typeof(InteractiveMixin))]
	[RequireComponent(typeof(AudioSource))]
	public class LightSwitchMixin : MonoBehaviour
	{
		[SerializeField] SwitchableLightMixin[] lights;

		[SerializeField] string switchedOffSubtitles;
		[SerializeField] string switchedOnSubtitles;

		private AudioSource clicking;

		Animator toggleAnimator;

		protected InteractiveMixin interactive;

		void Awake()
		{
			clicking = GetComponent<AudioSource>();
			toggleAnimator = GetComponent<Animator>();
			interactive = GetComponent<InteractiveMixin>();
		}

		void Start()
		{
			bool isOn = lights.Where(light => light.IsOn()).Count() > 0;
			// Sync all lights to be in the same switch state
			foreach (SwitchableLightMixin light in lights)
				light.lightIsOn = isOn;

			toggleAnimator.SetBool("TurnedOn", isOn);
			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
		}

		public void OnInteracted()
		{
			foreach (SwitchableLightMixin light in lights)
				light.lightIsOn = !light.lightIsOn;

			clicking.Play();

			toggleAnimator.SetBool("TurnedOn", !toggleAnimator.GetBool("TurnedOn"));

			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
		}
	}
}