using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
				enabled = gameObject.activeSelf
			};

			// Hack for the player object to save the correct orientation
			if (gameObject.tag == "Player")
			{
				FirstPersonController fps = gameObject.GetComponent<FirstPersonController>();

				GameState.current.objectPropertieDict[gameObject.name].rotation = fps.GetOrientation();

				return;
			}
			else
				GameState.current.objectPropertieDict[gameObject.name].rotation = gameObject.transform.rotation;

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
			gameObject.SetActive(properties.enabled);

			// Hack for the player object to set the correct orientation
			if (gameObject.tag == "Player")
			{
				FirstPersonController fps = gameObject.GetComponent<FirstPersonController>();

				fps.SetOrientation(properties.rotation);

				return;
			}
			else
				gameObject.transform.rotation = properties.rotation;

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