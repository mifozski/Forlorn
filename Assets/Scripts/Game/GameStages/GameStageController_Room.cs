#pragma warning disable 649

using UnityEngine;

namespace Forlorn
{
	public class GameStageController_Room : MonoBehaviour
	{
		[SerializeField] CutsceneMixin schoolShootingCutscene;

		[SerializeField] Transform paperPiece;

		void Start()
		{
			var we = GameState.current.cutscenesPlayed.Contains("wewe");
			var wewe = Cutscene.SchoolShooting.ToString();
			if (GameState.current.cutscenesPlayed.Contains(Cutscene.SchoolShooting.ToString()) == false)
				CutsceneController.PlayCutscene(schoolShootingCutscene);
		}

		void Update()
		{

		}
	}
}
