﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

namespace Forlorn
{
	public class GameStageController_Room : MonoBehaviour
	{
		[SerializeField] CutsceneMixin schoolShootingCutscene;

		[SerializeField] Transform paperPiece;

		void Start ()
		{
			var we = GameState.current.cutscenesPlayed.Contains("wewe");
			var wewe = Cutscene.SchoolShooting.ToString();
			if (GameState.current.cutscenesPlayed.Contains(Cutscene.SchoolShooting.ToString()) == false)
				EventController_Cutscene.Instance.TriggerEvent("PlayCutscene", schoolShootingCutscene);
		}

		void Update ()
		{

		}
	}
}
