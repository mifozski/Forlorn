using UnityEngine;
using StateMachine;

namespace Forlorn
{
	public enum GameStage
	{
		Init,
		AfterSchoolShootingCustcene,
		AfterFirstWakeUp,
		AfterCheckingTheDoorMonitor,
		AfterCheckingBackAlleyPaperPiece
	}

	public enum Cutscene
	{
		SchoolShooting
	}

	public class GameStageController : SingletonMonoBehavior<GameStageController>
	{
		StateMachine<GameStage> stateMachine;

		[SerializeField] GameStage initialState;

		void Awake()
		{
			Init();
		}

		private void Init()
		{
			stateMachine = StateMachine<GameStage>.Initialize(this);

			stateMachine.ChangeState(initialState);
		}

		void Start()
		{
		}

		public void SetStage(GameStage stage)
		{
			Debug.Log("GameStageController:SetState(): state = " + stage);
			stateMachine.ChangeState(stage);

			// currentStateDebugText.text = $"Current state: {state.ToString()}";


			// if (stage != Stage.Init)
			// {
			GameState.current.stage = stage;
			// SaveLoadGame.Save();
			// }
		}

		public GameStage GetStage()
		{
			return stateMachine.State;
		}

		void Init_Enter()
		{
			// TODO: Play cutscene
			// Debug.Log("GameStageController:Init_Enter()");

			// stateMachine.ChangeState(initialState);
		}

		public void OnInteracted(InteractiveObjectType objectType)
		{
			Debug.Log($"Interacted with {objectType.ToString()}");

			switch (objectType)
			{
				case InteractiveObjectType.DoorMonitor: OnUse_DoorMonitor(); break;
				case InteractiveObjectType.RoomDoor: OnUse_EntranceDoor(); break;
				case InteractiveObjectType.NoteUnderTheDoor: OnUse_PaperPiece(); break;
				case InteractiveObjectType.Notebook: OnUse_Notebook(); break;

				default: break;
			}
		}

		void AfterFirstWakeUp_Enter()
		{
			Debug.Log("GameStageController:AfterFirstWakeUp_Enter()");
		}

		private void OnUse_DoorMonitor()
		{
			// if (GetStage() == GameStage.AfterSchoolShootingCustcene)
			// {
			// 	SetStage(GameStage.AfterCheckingTheDoorMonitor);

			// 	// paperPiece.gameObject.SetActive(true);
			// }
		}

		private void OnUse_PaperPiece()
		{
			// if (GetStage() == GameStage.AfterCheckingTheDoorMonitor)
			// {
			// 	SetStage(GameStage.AfterCheckingBackAlleyPaperPiece);

			// 	GameController.ShowSubtitles("There's something for you in the bin in the back alley");
			// }
		}

		private void OnUse_EntranceDoor()
		{
			// if (GetStage() < GameStage.AfterCheckingBackAlleyPaperPiece)
			// 	GameController.ShowSubtitles("I don't want to go outside at this time");
			// else
			// 	GameController.LoadScene(Scenes.Entrance);
		}

		private void OnUse_Notebook()
		{
			Debug.Log("OnUse_Notebook");
			SubtitleController.Instance.ShowSubtitles("The notebook contains details about my ... about my work. I'd rather not look at it at the moment.");
		}

		void OnGUI()
		{
			// GUI.Label(new Rect(10, 10, 200, 50), $"Current state: {GetStage().ToString()}");
		}
	}
}