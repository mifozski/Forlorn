using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Forlorn;
using Serialization;

namespace Forlorn
{
	public enum Scenes
	{
		Room,
		Entrance,
		Restroom,
		Hallway
	}

	public class GameController : SingletonMonoBehavior<GameController>
	{
		[SerializeField] PersistenceController persistenceController;

		private GameStageController stageController;

		[SerializeField] Animator fadeInOutScreenAnimator;

		[SerializeField]
		TMPro.TextMeshProUGUI subtitles;
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
		}

		void Start()
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

		static public void LoadScene(Scenes scene)
		{
			Instance.fadeInOutScreenAnimator.SetBool("FadeOut", true);
		}

		static private string GetSceneName(Scenes scene)
		{
			switch (scene)
			{
			case Scenes.Room: return "Room";
			case Scenes.Entrance: return "Entrance";
			case Scenes.Restroom: return "Restroom";
			case Scenes.Hallway: return "Hallway";
			default: return "";
			}
		}

		static public void ShowSubtitles(string text)
		{
			if (subtitlesFadeOutCourutine != null)
					Instance.StopCoroutine(subtitlesFadeOutCourutine);

			Instance.subtitles.text = text;

			subtitlesFadeOutCourutine = Instance.StartCoroutine(Utils.FadeOutText(Instance.subtitles, 6f, 0f));
		}

		void OnApplicationQuit()
		{
			// SaveLoadGame.Save();
		}

		public void Save()
		{
			PersistenceController.Save();
		}

		public void Exit()
		{
			Application.Quit();
		}
	}
}