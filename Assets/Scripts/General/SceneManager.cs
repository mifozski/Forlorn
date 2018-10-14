using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	public class SceneManager
	{
		// [SerializeField] Animator fadeInOutScreenAnimator;

		Redux.Store _store;
		public Redux.Store store
		{
			set
			{
				_store = value;
				UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
				UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;
			}
			get
			{
				return _store;
			}
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			store.dispatch(SceneManagement.ActionCreators.setSceneLoaded(scene.buildIndex, true));
		}

		void OnSceneUnloaded(Scene scene)
		{
			store.dispatch(SceneManagement.ActionCreators.setSceneLoaded(scene.buildIndex, false));
		}

		public void LoadScene(int sceneId)
		{
			if (GetLoadedScenes().Contains(sceneId) == false)
				UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId, LoadSceneMode.Additive);
		}

		List<int> GetLoadedScenes()
		{
			return (store.getStateTree()[Reducers.scene] as Reducers.SceneState).loadedScenes;
		}
	}
}