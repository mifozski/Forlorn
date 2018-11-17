using UnityEngine;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider), typeof(InteractiveMixin))]
	[RequireComponent(typeof(AudioSource))]
	public class LightSwitchMixin : MonoBehaviour
	{
		[SerializeField] new SwitchableLightMixin light;

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
			toggleAnimator.SetBool("TurnedOn", light.IsOn());
			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
		}

		public void OnInteracted()
		{
			light.ToggleLight();

			clicking.Play();

			toggleAnimator.SetBool("TurnedOn", !toggleAnimator.GetBool("TurnedOn"));

			interactive.onHoverSubtitles = toggleAnimator.GetBool("TurnedOn") ? switchedOnSubtitles : switchedOffSubtitles;
		}
	}
}