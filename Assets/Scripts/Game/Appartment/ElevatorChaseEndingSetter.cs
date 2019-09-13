using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forlorn.Game
{
	public class ElevatorChaseEndingSetter : MonoBehaviour
	{
		[SerializeField] Transform wakeUpPoint;

		// Start is called before the first frame update
		void Start()
		{

		}

		public void Set()
		{
			GameController.Instance.player.SetPositionAndRotation(wakeUpPoint.position, wakeUpPoint.rotation);
		}
	}
}
