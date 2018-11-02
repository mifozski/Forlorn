using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

namespace Forlorn
{
	public class GameStageController_Room : MonoBehaviour
	{
		[SerializeField] CutsceneMixin schoolShootingCutscene;

		[SerializeField] Transform paperPiece;

		void Awake()
		{

		}

		void Start ()
		{
			EventController_Cutscene.Instance.TriggerEvent("PlayCutscene", schoolShootingCutscene);
		}

		void Update ()
		{

		}
	}
}
