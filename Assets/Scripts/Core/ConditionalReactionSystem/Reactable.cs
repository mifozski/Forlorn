#pragma warning disable 649

using UnityEngine;
using UnityEngine.Events;

using Forlorn.Events;

namespace Forlorn
{
	public class Reactable : MonoBehaviour
	{
		[SerializeField] string listenedReaction;
		[SerializeField] UnityEvent action;

		void Start()
		{
			StringEventManager.Instance.StartListening("invokeReaction", OnReactionInvoked);
		}

		void OnReactionInvoked(string reactionId)
		{
			// Debug.Log($"Checking {reactionId} against ")
			if (reactionId == listenedReaction)
			{
				action.Invoke();
			}
		}
	}
}