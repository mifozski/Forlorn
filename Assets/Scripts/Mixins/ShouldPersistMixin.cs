using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class ShouldPersistMixin : MonoBehaviour
	{
		[SerializeField] bool saveChildren = true;

		void Start()
		{
			EventController_PersistentObject.Instance.TriggerEvent("ObjectCreated", this);

			Load();
		}

		public void Save()
		{
			Debug.Log($"ON SAVE for {gameObject.name}");

			SaveObject(gameObject);
		}

		private void SaveObject(GameObject gameObject)
		{
			MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();

			GameState.current.objectPropertieDict[gameObject.name] = new GameObjectProperties {
				visible = renderer ? renderer.enabled : true,
				position = gameObject.transform.position,
				rotation = gameObject.transform.rotation,
				enabled = gameObject.activeSelf
			};

			if (saveChildren)
			{
				foreach (Transform child in gameObject.transform)
				{
					SaveObject(child.gameObject);
				}
			}
		}

		public void Load()
		{
			LoadObject(gameObject);
		}

		void LoadObject(GameObject gameObject)
		{
			if (GameState.current.objectPropertieDict.ContainsKey(gameObject.name) == false)
				return;

			GameObjectProperties properties = GameState.current.objectPropertieDict[gameObject.name];

			MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
			if (renderer)
				renderer.enabled = properties.visible;

			gameObject.transform.position = properties.position;
			gameObject.transform.rotation = properties.rotation;
			gameObject.SetActive(properties.enabled);

			if (saveChildren)
			{
				foreach (Transform child in gameObject.transform)
				{
					LoadObject(child.gameObject);
				}
			}
		}
	}
}