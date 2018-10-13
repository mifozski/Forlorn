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

			mainMenu.store = store;
			Debug.Log("SETTING THE FUNCKING STORE");
			playerController.store = store;

			loaderSaver = new GameStateLoaderSaver();
		}

		void Start()
		{
			bool loaded = loaderSaver.Load();

			if (loaded)
			{
				Debug.Log($"sceneId: {GameState.current.sceneId}");
				store.dispatch(SceneManagement.ActionCreators.loadScene(GameState.current.sceneId));
			}
			else
			{
				Debug.Log($"sceneId: NOT FOUND");
				store.dispatch(SceneManagement.ActionCreators.loadScene(defaultSceneId));
			}
		}
	}
}
