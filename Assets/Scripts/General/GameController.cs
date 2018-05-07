using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Forlorn;

namespace Forlorn {

public enum Scene
{
	Room,
	Entrance,
	Restroom,
	Hallway
}

public class GameController : MonoBehaviour {

	public static GameController gameController;

	private GameStageController stageController;

	public static GameController instance
	{
		get
		{
			if (!gameController)
			{
				gameController = FindObjectOfType (typeof (GameController)) as GameController;

				if (!gameController)
				{
					Debug.LogError ("There needs to be one active GameController script on a GameObject in your scene.");
				}
				else
				{
					gameController.Init ();
				}
			}

			return gameController;
		}
	}

	void Awake()
	{
		if (gameController == null)
		{
			DontDestroyOnLoad(gameObject);
			gameController = this;

			stageController = new GameStageController();

			Init();
		}
		else if (gameController != this)
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
	}

	static public void LoadScene(Scene scene)
	{
		SceneManager.LoadScene(GetSceneName(scene));
	}

	static private string GetSceneName(Scene scene)
	{
		switch (scene)
		{
		case Scene.Room: return "Room";
		case Scene.Entrance: return "Entrance";
		case Scene.Restroom: return "Restroom";
		case Scene.Hallway: return "Hallway";
		default: return "";
		}
	}

	static public void SaveGame()
	{
		GameState state = new GameState();
		state.stage = 1;
		state.Save();

		// GameStateSaver saver = new GameStateSaver();
		// saver.Save(state);

	}
}
}