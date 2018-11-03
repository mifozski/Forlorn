using UnityEngine;
using UnityEngine.Playables;

using Forlorn;

namespace Forlorn
{
	public class CutsceneController : SingletonMonoBehavior<GameStageController>
	{
		private PlayableDirector cutscene;

		Redux.Store _store;
		public Redux.Store store
		{
			set
			{
				_store = value;
			}
			get
			{
				return _store;
			}
		}

		void Awake()
		{
			EventController_Cutscene.Instance.StartListening("PlayCutscene", OnPlayCutscene);
		}

		private void OnPlayCutscene(CutsceneMixin cutsceneMixin)
		{
			PlayableDirector director = cutsceneMixin.gameObject.GetComponentInChildren<PlayableDirector>();
			PlayCutscene(director);
		}

		public void PlayCutscene(PlayableDirector _cutscene)
		{
			cutscene = _cutscene;

			cutscene.stopped += OnStopped;

			cutscene.Play();

			store.dispatch(ActionCreators.Cutscenes.setCutsceneStarted());
		}

		private void OnStopped(PlayableDirector director)
		{
			if (cutscene == director)
			{
				CutsceneMixin mixin = director.gameObject.GetComponentInParent<CutsceneMixin>();
				if (mixin == null)
					Debug.LogError($"No Cutscene mixin on Director object {director.name}");

				if (mixin)
				{
					store.dispatch(ActionCreators.Cutscenes.setCutsceneFinished(mixin.cutscene));
					if (GameState.current.cutscenesPlayed.Contains(mixin.cutscene.ToString()) == false)
						GameState.current.cutscenesPlayed.Add(mixin.cutscene.ToString());
				}
			}
		}
	}
}