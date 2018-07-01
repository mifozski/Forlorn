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
	public int scene;

	public Vector3 playerPosition;
	public Vector3 playerOrientation;

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