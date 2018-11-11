using UnityEngine;
using UnityEngine.Events;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider))]
	public class LightSwitchMixin : MonoBehaviour
	{
		public UnityEvent switchingEvent;

		Animator doorAnimator;

		void Start()
		{
			doorAnimator = GetComponent<Animator>();
		}

		public void OnInteracted()
		{
			doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));
		}
	}
}