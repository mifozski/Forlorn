using UnityEngine;
using UnityEngine.Playables;

using Forlorn;

namespace Forlorn
{
	static public class CutsceneController
	{
		static private PlayableDirector cutscene;

		static public void PlayCutscene(CutsceneMixin cutsceneMixin)
		{
			cutscene = cutsceneMixin.gameObject.GetComponentInChildren<PlayableDirector>();

			cutscene.stopped += OnStopped;

			cutscene.Play();
		}

		static private void OnStopped(PlayableDirector director)
		{
			if (cutscene == director)
			{
				CutsceneMixin mixin = director.gameObject.GetComponentInParent<CutsceneMixin>();
				if (mixin == null)
				{
					Debug.LogError($"No Cutscene mixin on Director object {director.name}");
				}

				if (mixin)
				{
					if (GameState.current.cutscenesPlayed.Contains(mixin.cutscene.ToString()) == false)
					{
						GameState.current.cutscenesPlayed.Add(mixin.cutscene.ToString());
					}
				}
			}
		}
	}
}
