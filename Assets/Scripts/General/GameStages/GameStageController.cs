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

public class GameStageController : MonoBehaviour
{
	StateMachine<State> fsm;

	GameStageController c = null;

	Transform paperPiece;
	Animator paperPieceAnimator;

	void Awake()
	{
		if (c == null)
		{
			DontDestroyOnLoad(gameObject);

			c = this;

			Init();
		}
		else if (c != this)
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
		fsm = StateMachine<State>.Initialize(this);

		SetState(State.Init);

		paperPiece = GameObject.Find("PaperPiece").transform;
		paperPieceAnimator = paperPiece.GetComponent<Animator>();

		EventManager.StartListening("OnUse_DoorMonitor", OnUse_DoorMonitor);
		EventManager.StartListening("OnUse_PaperPiece", OnUse_PaperPiece);
		
		EventManager.StartListening("OnUse_EntranceDoor", OnUse_EntranceDoor);
	}

	public void SetState(State state)
	{
		Debug.Log("GameStageController:Init_Enter(): state=" + state);
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

	void AfterFirstWakeUp_Enter()
	{
		Debug.Log("GameStageController:AfterFirstWakeUp_Enter()");
	}

	private void OnUse_DoorMonitor()
	{
		if (GetState() == State.AfterFirstWakeUp)
		{
			SetState(State.AfterCheckingTheDoorMonitor);

			// Slide paper piece under the door
			paperPieceAnimator.Play("PaperPieceSlide");
		}
	}

	private void OnUse_PaperPiece()
	{
		if (GetState() == State.AfterCheckingTheDoorMonitor)
		{
			SetState(State.AfterCheckingBackAlleyPaperPiece);
		}
	}

	private void OnUse_EntranceDoor()
	{
		GameController.LoadScene(Scene.Entrance);
	}
}
}