#pragma warning disable 649

using UnityEngine;

using Serialization;

namespace Forlorn
{
	public class DoorMixin : MonoBehaviour, OnDeserializedCallback
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
			UpdateSubtitles();
		}

		public void OnInteracted()
		{
			doorAnimator.SetBool(isOpenParamKey, !IsOpen());
			UpdateSubtitles();
		}

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

		private void UpdateSubtitles()
		{
			interactive.onHoverSubtitles = doorAnimator.GetBool(isOpenParamKey) ? openedUpSubtitles : closedUpSubtitles;
		}

		private bool IsOpen()
		{
			return doorAnimator.GetBool(isOpenParamKey);
		}

		public void OnDeserialized()
		{
			UpdateSubtitles();
		}
	}
}