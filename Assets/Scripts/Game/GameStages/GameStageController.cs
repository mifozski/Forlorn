using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

using Forlorn;

namespace Forlorn {
public enum State
{
	Init,
	AfterFirstWakeUp,
	AfterCheckingTheDoorMonitor,
	AfterCheckingBackAlleyPaperPiece
}

public class GameStageController : SingletonMonoBehavior<GameStageController>
{
	StateMachine<State> fsm;

	[SerializeField] Transform paperPiece;

	void Awake()
	{
		Init();
	}

	private void Init()
	{
		fsm = StateMachine<State>.Initialize(this);

		SetState(State.Init);
	}

	public void SetState(State state)
	{
		Debug.Log("GameStageController:Init_Enter(): state = " + state);
		fsm.ChangeState(state);
	}

	public State GetState()
	{
		return fsm.State;
	}

	void Init_Enter()
	{
		// TODO: Play cutscene
		Debug.Log("GameStageController:Init_Enter()");

		fsm.ChangeState(State.AfterFirstWakeUp);
	}

	public void OnInteracted(InteractiveObjectType objectType)
	{
		switch (objectType)
		{
			case InteractiveObjectType.DoorMonitor: OnUse_DoorMonitor(); break;
			case InteractiveObjectType.RoomDoor: OnUse_EntranceDoor(); break;
			case InteractiveObjectType.NoteUnderTheDoor: OnUse_PaperPiece(); break;

			default: break;
		}
	}

	void AfterFirstWakeUp_Enter()
	{
		Debug.Log("GameStageController:AfterFirstWakeUp_Enter()");
	}

	private void OnUse_DoorMonitor()
	{
		if (GetState() == State.AfterFirstWakeUp)
		{
			SetState(State.AfterCheckingTheDoorMonitor);

			paperPiece.gameObject.SetActive(true);

			// Slide paper piece under the door
			// paperPieceAnimator.Play("PaperPieceSlide");
		}
	}

	private void OnUse_PaperPiece()
	{
		if (GetState() == State.AfterCheckingTheDoorMonitor)
		{
			SetState(State.AfterCheckingBackAlleyPaperPiece);

			GameController.ShowSubtitles("There's something for you in the bin in the back alley");
		}
	}

	private void OnUse_EntranceDoor()
	{
		if (GetState() < State.AfterCheckingBackAlleyPaperPiece)
			GameController.ShowSubtitles("I don't want to go outside at this time");
		else
			GameController.LoadScene(Scene.Entrance);
	}
}
}