#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

using Serialization;
using Forlorn.Core.ConditionSystem;

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
		[SerializeField] ConditionalReactionSystem conditionalReactionSystem;
		[SerializeField] SceneController sceneController;

		private GameStageController stageController;

		[SerializeField] Animator fadeInOutScreenAnimator;

		[SerializeField] public Transform player;

		[SerializeField] Image interactableObjectIndicator;
		static bool interactableObjectIndicatorIsShown = false;
		static Coroutine interactableObjectIndicatorCoroutine = null;

		private string gameStateKey = "game_state";

		void Awake()
		{
			bool haveDataSave = persistenceController.Deserialize();
			if (haveDataSave)
			{
				var qwe = persistenceController.GetDeserializedData();
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