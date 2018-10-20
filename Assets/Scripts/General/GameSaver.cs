using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Forlorn;

namespace Forlorn
{
	public class GameSaver :  SingletonMonoBehavior<GameSaver>
	{
		Redux.Store _store;
		public Redux.Store store
		{
			set { _store = value; }
			get { return _store; }
		}

		GameStateLoaderSaver saver = new GameStateLoaderSaver();

		private List<ShouldPersistMixin> persistentObjects = new List<ShouldPersistMixin>();

		void Awake()
		{
			EventController_PersistentObject.Instance.StartListening("ObjectCreated", OnCreatedPersistentObject);
		}

		public void Save()
		{
			persistentObjects.ForEach(persistentObject => persistentObject.Save());

			saver.Save();
		}

		public void OnCreatedPersistentObject(ShouldPersistMixin persistentObject)
		{
			Debug.Assert(persistentObjects.Contains(persistentObject) == false);

			persistentObjects.Add(persistentObject);
		}
	}
}