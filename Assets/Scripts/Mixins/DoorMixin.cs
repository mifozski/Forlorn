using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class DoorMixin : MonoBehaviour
	{
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