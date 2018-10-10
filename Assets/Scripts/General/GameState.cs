using UnityEngine;
using Forlorn;

namespace Forlorn
{
	[System.Serializable]
	public class GameState
	{
		public bool loaded = false;

		public static GameState current;

		public GameStage stage;
		public int sceneId;

		public Vector3 playerPosition;
		public Vector3 playerOrientation;
	}

}