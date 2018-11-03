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

		[SerializeField] CutsceneController cutsceneController;

		[SerializeField] MainMenu mainMenu;
		[SerializeField] PlayerController playerController;

		[SerializeField] GameStateInspector m_GameStateInspector;

		Reducers.SceneState prevSceneState = new Reducers.SceneState();

		private List<ShouldPersistMixin> persistentObjects = new List<ShouldPersistMixin>();

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
			cutsceneController.store = store;

			mainMenu.store = store;
			playerController.store = store;

			loader.Load(ref GameState.current);
			if (GameState.current == null)
				GameState.current = new GameState();

			m_GameStateInspector.gameState = GameState.current;
		}

		void Start()
		{
			if (GameState.current.sceneId != -1)
			{
				Debug.Log($"sceneId: {GameState.current.sceneId}");

				sceneManager.LoadScene(GameState.current.sceneId);
			}
			else
			{
				Debug.Log($"sceneId: NOT FOUND");

				sceneManager.LoadScene(defaultSceneId);
			}
		}

		void OnStateChange(Redux.Store store)
		{
			Redux.StateTree state = store.getStateTree();

			var sceneState = state[Reducers.scene] as Reducers.SceneState;
			if (prevSceneState != sceneState)
			{
				prevSceneState = Utils.DeepCopy<Reducers.SceneState>(sceneState);
			}
		}

		public void OnCreatedPersistentObject(ShouldPersistMixin persistentObject)
		{
			Debug.Assert(persistentObjects.Contains(persistentObject) == false);

			persistentObjects.Add(persistentObject);
		}
	}
}
