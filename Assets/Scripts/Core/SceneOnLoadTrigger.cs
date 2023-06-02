using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Forlorn
{
	public class SceneOnLoadTrigger : MonoBehaviour
	{
		public UnityEvent OnLoad;

		private void Awake()
		{
			OnLoad?.Invoke();
			// SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
			// {
			// 	if (_triggered != true/*  && scene == gameObject.scene */)
			// 	{
			// 		_triggered = true;

			// 		OnLoad.Invoke();
			// 	}
			// };
		}

		bool _triggered = false;
	}
}
