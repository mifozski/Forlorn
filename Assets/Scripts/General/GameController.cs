﻿#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

using Serialization;
using Forlorn.Core.ConditionSystem;
using UnityEngine.SceneManagement;

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
		[SerializeField] bool _skipDeserializing;

		[SerializeField] PersistenceController persistenceController;
		[SerializeField] ConditionalReactionSystem conditionalReactionSystem;
		[SerializeField] SceneController sceneController;

		private GameStageController stageController;

		[SerializeField] Animator fadeInOutScreenAnimator;

		[SerializeField] public Transform player;

		[SerializeField] Image interactableObjectIndicator;
		static bool interactableObjectIndicatorIsShown = false;
		static Coroutine interactableObjectIndicatorCoroutine = null;

		private string gameStateKey = "game_state";

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void StaticInitialize()
		{
			Initialize();
		}

		void Awake()
		{
			if (!_skipDeserializing)
			{
				bool haveDataSave = persistenceController.Deserialize();
				if (haveDataSave)
				{
					GameState deserializedState = persistenceController.GetDeserializedData().genericObjects[gameStateKey] as GameState;

					GameState.current = new GameState();
					GameState.current.ReconcileStates(deserializedState);
					conditionalReactionSystem.SetVariables(GameState.current.variables);

					PersistenceController.AddSerializedObject(gameStateKey, GameState.current);
				}
				else
				{
					Debug.Log("No saved data found.");
					GameState.current = new GameState();
				}
				PersistenceController.AddSerializedObject(gameStateKey, GameState.current);
			}
			else
			{
				GameState.current = new GameState();
				SceneManager.sceneLoaded += OnSceneLoaded;
			}
		}

		void Start() { }

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			var startingPoint = GameObject.FindGameObjectWithTag("StartingPoint");
			if (startingPoint != null)
			{
				player.transform.SetPositionAndRotation(startingPoint.transform.position, startingPoint.transform.rotation);
			}
		}

		public static void ShowInteractableObjectIndicator(bool show)
		{
			if (show != interactableObjectIndicatorIsShown)
			{
				if (interactableObjectIndicatorCoroutine != null)
					Instance.StopCoroutine(interactableObjectIndicatorCoroutine);

				interactableObjectIndicatorCoroutine = Instance.StartCoroutine(Utils.AlphaFade(show, 1f, Instance.interactableObjectIndicator.material));

				interactableObjectIndicatorIsShown = show;
			}
		}

		public static void ShowPressToActivateProgressBar(bool show, float progress)
		{
			if (show != interactableObjectIndicatorIsShown)
			{
				if (interactableObjectIndicatorCoroutine != null)
					Instance.StopCoroutine(interactableObjectIndicatorCoroutine);

				interactableObjectIndicatorCoroutine = Instance.StartCoroutine(Utils.AlphaFade(show, 1f, Instance.interactableObjectIndicator.material));

				interactableObjectIndicatorIsShown = show;
			}
		}

		static private string GetSceneName(Scenes scene)
		{
			switch (scene)
			{
				case Scenes.Room:
					return "Room";
				case Scenes.Entrance:
					return "Entrance";
				case Scenes.Restroom:
					return "Restroom";
				case Scenes.Hallway:
					return "Hallway";
				default:
					return "";
			}
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
