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

		Animator doorAnimator;

		void Awake()
		{
			clicking = GetComponent<AudioSource>();
		}

		void Start()
		{
			// doorAnimator = GetComponent<Animator>();
		}

		public void OnInteracted()
		{
			light.ToggleLight();

			clicking.Play();
			// doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));
		}
	}
}