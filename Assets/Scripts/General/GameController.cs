using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Forlorn;

namespace Forlorn
{
public enum Scene
{
	Room,
	Entrance,
	Restroom,
	Hallway
}

public class GameController : SingletonMonoBehavior<GameController>
{
	public static GameController gameController;

	private GameStageController stageController;

	[SerializeField]
	Image interactableObjectIndicator;
	static bool interactableObjectIndicatorIsShown = false;
	static Coroutine interactableObjectIndicatorCoroutine = null;

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

	public static void ShowInteractableObjectIndicator(bool draw)
	{
		if (draw != interactableObjectIndicatorIsShown)
		{
			if (interactableObjectIndicatorCoroutine != null)
				Instance.StopCoroutine(interactableObjectIndicatorCoroutine);

			interactableObjectIndicatorCoroutine = Instance.StartCoroutine(Utils.AlphaFade(draw, 1f, Instance.interactableObjectIndicator.material));

			interactableObjectIndicatorIsShown = draw;
		}
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