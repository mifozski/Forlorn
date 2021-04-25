#pragma warning disable 649

using UnityEngine;
using UnityEngine.Experimental.Animations;
using UnityEngine.Playables;

using Serialization;

namespace Forlorn
{
	public class DoorMixin : MonoBehaviour, OnDeserializedCallback
	{
		Animator _doorAnimator;
		AudioSource soundSource;
		InteractiveMixin interactive;

		[SerializeField] string openedUpSubtitles;
		[SerializeField] string closedUpSubtitles;
		[SerializeField] AudioClip openingSound;
		[SerializeField] AudioClip closingSound;
		[SerializeField] PlayableAsset _openPlayable;
		[SerializeField] PlayableAsset _closePlayable;
		[SerializeField] PlayableDirector _director;

		private readonly string isOpenParamKey = "IsOpen";

		void Awake()
		{
			_doorAnimator = GetComponentInParent<Animator>();
			if (_doorAnimator == null)
			{
				Debug.LogError("No animator on a door");
			}
			interactive = GetComponentInChildren<InteractiveMixin>();
			soundSource = GetComponentInChildren<AudioSource>();
		}

		void Start()
		{
			UpdateSubtitles();
		}

		public void OnInteracted()
		{
			if (_closed)
			{
				_director.playableAsset = _openPlayable;
			}
			else
			{
				_director.playableAsset = _closePlayable;
			}

			_closed = !_closed;

			_director.RebuildGraph();
			_director.time = 0.0f;
			_director.Play();

			// doorAnimator.SetBool(isOpenParamKey, !IsOpen());
			UpdateSubtitles();
		}

		private void UpdateSubtitles()
		{
			interactive.onHoverSubtitles = _doorAnimator.GetBool(isOpenParamKey) ? openedUpSubtitles : closedUpSubtitles;
		}

		private bool IsOpen()
		{
			return _doorAnimator.GetBool(isOpenParamKey);
		}

		public void OnDeserialized()
		{
			UpdateSubtitles();
		}

		bool _closed = true;
	}
}
