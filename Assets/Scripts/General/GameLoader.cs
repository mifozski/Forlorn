using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Forlorn;

namespace Forlorn
{
	public class GameLoader : MonoBehaviour
	{
		GameStateLoaderSaver loaderSaver;

		Redux.Store store;

		SceneManager sceneManager = new SceneManager();

		[SerializeField] int defaultSceneId;

		[SerializeField] MainMenu mainMenu;
		[SerializeField] PlayerController playerController;

		void Awake()
		{
			// TODO: Create store externally and pass it as an argument to GameLoader's constructor
			Redux.FinalReducer finalReducer = Redux.combineReducers(new Redux.Reducer[] {
				Reducers.general,
                Reducers.scene,
            });
            store = Redux.createStore(finalReducer);

			sceneManager.store = store;
			mainMenu.store = store;
			playerController.store = store;

			loaderSaver = new GameStateLoaderSaver();

			// Debug - Store scenes loaded in the editor in the store

		}

		void Start()
		{
			bool loaded = loaderSaver.Load();

			if (loaded)
			{
				Debug.Log($"sceneId: {GameState.current.sceneId}");

				sceneManager.LoadScene(GameState.current.sceneId);
				// store.dispatch(SceneManagement.ActionCreators.loadScene(GameState.current.sceneId));
			}
			else
			{
				Debug.Log($"sceneId: NOT FOUND");

				sceneManager.LoadScene(defaultSceneId);
				// store.dispatch(SceneManagement.ActionCreators.loadScene(defaultSceneId));
			}
		}
	}
}
