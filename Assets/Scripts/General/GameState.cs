using UnityEngine;
using Forlorn;

namespace Forlorn{


class GameState
{
	public int stage;
	public int scene;

	public Vector3 playerPosition;
	public Quaternion playerOrientation;

	public void Save()
	{
		string json = JsonUtility.ToJson(this);
		Debug.Log(json);
	}
}

class GameStateSaver
{
	public void Save(GameState state)
	{
		string json = JsonUtility.ToJson(state);


	}
}
}