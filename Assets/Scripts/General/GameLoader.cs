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

		void Awake()
		{
			loaderSaver = new GameStateLoaderSaver();
		}

		void Start()
		{
			bool loaded = loaderSaver.Load();

			if (loaded)
			{
				Debug.Log($"sceneId: {GameState.current.sceneId}");
				SceneManager.LoadScene(GameState.current.sceneId, LoadSceneMode.Additive);
			}
			else
			{
				Debug.Log($"sceneId: NOT FOUND");
				SceneManager.LoadScene(1, LoadSceneMode.Additive);
			}
		}
	}
}
