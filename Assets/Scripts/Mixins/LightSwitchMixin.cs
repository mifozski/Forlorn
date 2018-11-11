using UnityEngine;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider), typeof(InteractiveMixin))]
	[RequireComponent(typeof(AudioSource))]
	public class LightSwitchMixin : MonoBehaviour
	{
		[SerializeField] new SwitchableLightMixin light;

		private AudioSource clicking;

		Animator toggleAnimator;

		void Awake()
		{
			clicking = GetComponent<AudioSource>();
			toggleAnimator = GetComponent<Animator>();
		}

		void Start()
		{
			toggleAnimator.SetBool("TurnedOn", light.IsOn());
		}

		public void OnInteracted()
		{
			light.ToggleLight();

			clicking.Play();

			toggleAnimator.SetBool("TurnedOn", !toggleAnimator.GetBool("TurnedOn"));
		}
	}
}