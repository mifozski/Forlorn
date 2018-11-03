using UnityEngine;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	[System.Serializable]
	public class GameObjectProperties
	{
		public bool visible;
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
		public int sceneId = 0;

		public List<string> cutscenesPlayed = new List<string>();

		public Dictionary<string, GameObjectProperties> objectPropertieDict = new Dictionary<string, GameObjectProperties>();
	}
}