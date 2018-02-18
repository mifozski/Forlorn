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

	public static GameController Controller;

	private GameStageController stageController;

	void Awake()
	{
		if (Controller == null)
		{
			DontDestroyOnLoad(gameObject);
			Controller = this;

			stageController = new GameStageController();

			Init();
		}
		else if (Controller != this)
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
	}

	public void LoadScene(Scene scene)
	{
		SceneManager.LoadScene(GetSceneName(scene));
	}

	private string GetSceneName(Scene scene)
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
}
}