using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class DoorMixin : MonoBehaviour
	{
		Animator doorAnimator;
		AudioSource soundSource;
		InteractiveMixin interactive;

		[SerializeField] string openedUpSubtitles;
		[SerializeField] string closedUpSubtitles;
		[SerializeField] AudioClip openingSound;
		[SerializeField] AudioClip closingSound;

		private string isOpenParamKey = "IsOpen";

		void Awake()
		{
			doorAnimator = GetComponentInParent<Animator>();
			if (doorAnimator == null)
				Debug.LogError("No animator on a door");
			interactive = GetComponentInChildren<InteractiveMixin>();
			soundSource = GetComponentInChildren<AudioSource>();
		}

		void Start()
		{
			interactive.onHoverSubtitles = IsOpen() ? openedUpSubtitles : closedUpSubtitles;
		}

		public void OnInteracted()
		{
			doorAnimator.SetBool(isOpenParamKey, !IsOpen());
			interactive.onHoverSubtitles = doorAnimator.GetBool(isOpenParamKey) ? openedUpSubtitles : closedUpSubtitles;
		}

		public void QEW(bool weqw) { }

		public void PlayOpeningSound()
		{
			if (IsOpen())
			{
				soundSource.PlayOneShot(openingSound);
			}
		}

		public void PlayClosingSound()
		{
			if (!IsOpen())
			{
				soundSource.PlayOneShot(closingSound);
			}
		}

		private bool IsOpen()
		{
			return doorAnimator.GetBool(isOpenParamKey);
		}
	}
}