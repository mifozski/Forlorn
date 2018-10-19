using UnityEngine;

using Forlorn;

namespace Forlorn
{
	public class ShouldPersistMixin : MonoBehaviour
	{
		[SerializeField] bool saveChildren = true;

		public void Save()
		{
			SaveObject(gameObject);
		}

		private void SaveObject(GameObject gameObject)
		{
			GameState.current.objectPropertieDict[gameObject.name] = new GameObjectProperties {
				position = transform.position,
				rotation = transform.rotation,
				enabled = gameObject.activeSelf
			};

			if (saveChildren)
			{
				foreach (Transform child in transform)
				{
					SaveObject(transform.gameObject);
				}
			}
		}

		public void Load()
		{
			LoadObject(gameObject);
		}

		void LoadObject(GameObject gameObject)
		{
			GameObjectProperties properties = GameState.current.objectPropertieDict[gameObject.name];

			transform.position = properties.position;
			transform.rotation = properties.rotation;
			gameObject.SetActive(properties.enabled);

			if (saveChildren)
			{
				foreach (Transform child in transform)
				{
					LoadObject(transform.gameObject);
				}
			}
		}
	}
}