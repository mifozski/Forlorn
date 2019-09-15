#pragma warning disable 649

using UnityEngine;

using Forlorn.Core.ConditionSystem;

namespace Forlorn.Game
{
	public class ElevatorChaseEndingSetter : MonoBehaviour
	{
		[SerializeField] Transform wakeUpPoint;

		ConditionalReactionSystem conditionalReactionSystem;

		// Start is called before the first frame update
		void Start()
		{
			conditionalReactionSystem = FindObjectOfType<ConditionalReactionSystem>();
		}

		public void Set()
		{
			GameController.Instance.player.SetPositionAndRotation(wakeUpPoint.position, wakeUpPoint.rotation);
			conditionalReactionSystem.SetVariable("ELEVATOR_CHASE", 2);
		}
	}
}
