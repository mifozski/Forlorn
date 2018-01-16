using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTAnimatorStateMachine;

public class FirstWakingUp : DTStateMachineBehaviour<int> {

	void OnInitialized()
	{
	}

	void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("FirstWakingUp::OnStateEntered()");
		AudioSource knockingSound = GameObject.Find("KnockingSound").GetComponent<AudioSource>();
		knockingSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
