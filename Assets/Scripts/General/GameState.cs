using UnityEngine;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	[System.Serializable]
	public class GameObjectProperties
	{
		public Vector3 position;
		public Quaternion rotation;
		public bool enabled;
	}

	[System.Serializable]
	public class GameState
	{
		public bool loaded = false;

		public static GameState current;

		public GameStage stage;
		public int sceneId;

		public Vector3 playerPosition;
		public Quaternion playerRotation;

		public List<GameObjectProperties> objectPropertiesList;
	}

}