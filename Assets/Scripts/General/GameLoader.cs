using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using Forlorn;

namespace Forlorn
{
	public class GameLoader : MonoBehaviour
	{
		GameStateLoaderSaver loader = new GameStateLoaderSaver();

		Redux.Store store;

		SceneController sceneManager = new SceneController();

		[SerializeField] int defaultSceneId;

		[SerializeField] MainMenu mainMenu;
		[SerializeField] PlayerController playerController;

		Reducers.SceneState prevSceneState = new Reducers.SceneState();

		void Awake()
		{
			// TODO: Create store externally and pass it as an argument to GameLoader's constructor
			Redux.FinalReducer finalReducer = Redux.combineReducers(new Redux.Reducer[] {
				Reducers.general,
                Reducers.scene,
            });
            store = Redux.createStore(finalReducer);

			store.subscribe(OnStateChange);

			sceneManager.store = store;
			mainMenu.store = store;
			playerController.store = store;
		}

		void Start()
		{
			bool loaded = loader.Load();

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

		void OnStateChange(Redux.Store store)
		{
			Redux.StateTree state = store.getStateTree();

			var sceneState = state[Reducers.scene] as Reducers.SceneState;
			if (prevSceneState != sceneState)
			{
				if (prevSceneState.loadedScenes != sceneState.loadedScenes)
				{
					int [] newScenes = sceneState.loadedScenes.Except(prevSceneState.loadedScenes).ToArray();
					if (newScenes.Length > 0 && newScenes[0] != 0)
					{
						Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(newScenes[0]);
						LoadSceneObjects(scene);
					}
				}

				prevSceneState = Utils.DeepCopy<Reducers.SceneState>(sceneState);
			}
		}

		void LoadSceneObjects(Scene scene)
		{
			GameObject [] sceneRootObjects = scene.GetRootGameObjects();

			foreach (GameObject obj in sceneRootObjects)
			{
				ShouldPersistMixin objLoader = obj.GetComponent<ShouldPersistMixin>();
				if (objLoader == null)
					continue;

				objLoader.Load();
			}
		}
	}
}
