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
	private GameStageController stageController;

	[SerializeField] Animator fadeInOutScreenAnimator;

	[SerializeField]
	Text subtitles;
	static Coroutine subtitlesFadeOutCourutine = null;

	[SerializeField] Image interactableObjectIndicator;
	static bool interactableObjectIndicatorIsShown = false;
	static Coroutine interactableObjectIndicatorCoroutine = null;

	void Awake()
	{
		Init();
	}

	private void Init()
	{
		// fadeInOutScreenAnimator = fadeInOutScreen.GetComponent<Animator>();
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
		Instance.fadeInOutScreenAnimator.SetBool("FadeOut", true);
		SceneManager.LoadScene(GetSceneName(scene));
	}

	// IEnumerator FadeOutScreen()
	// {
	// 	fadeInOutScreenAnimator.SetBool("Fade", true);
	// 	// yield return new WaitUntil(() => fadeInOutScreen.color.a == 1);

	// }

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

	static public void ShowSubtitles(string text)
	{
		if (subtitlesFadeOutCourutine != null)
				Instance.StopCoroutine(subtitlesFadeOutCourutine);

		Instance.subtitles.text = text;

		Instance.StartCoroutine(Utils.FadeOutText(Instance.subtitles, 6f, 0f));
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