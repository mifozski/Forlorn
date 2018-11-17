using UnityEngine;

using Forlorn;

namespace Forlorn
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(InteractiveMixin))]
	public class DoorMixin : MonoBehaviour
	{
		Animator doorAnimator;

		[SerializeField] string openedUpSubtitles;
		[SerializeField] string closedUpSubtitles;

		protected InteractiveMixin interactive;

		protected

		void Awake()
		{
			doorAnimator = GetComponent<Animator>();
			interactive = GetComponent<InteractiveMixin>();
		}

		void Start()
		{
			interactive.onHoverSubtitles = doorAnimator.GetBool("IsOpen") ? openedUpSubtitles : closedUpSubtitles;
		}

		public void OnInteracted()
		{
			doorAnimator.SetBool("IsOpen", !doorAnimator.GetBool("IsOpen"));
			interactive.onHoverSubtitles = doorAnimator.GetBool("IsOpen") ? openedUpSubtitles : closedUpSubtitles;
		}
	}
}