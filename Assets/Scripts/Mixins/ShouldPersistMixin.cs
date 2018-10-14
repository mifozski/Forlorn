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
			GameState.current.objectPropertiesList.Add(new GameObjectProperties {
				position = transform.position,
				rotation = transform.rotation,
				enabled = gameObject.activeSelf
			});

			if (saveChildren)
			{
				foreach (Transform child in transform)
				{
					SaveObject(transform.gameObject);
				}
			}
		}
	}
}