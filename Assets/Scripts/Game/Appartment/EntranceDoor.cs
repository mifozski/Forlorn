using UnityEngine;

namespace Forlorn.Game.Appartment
{
	public class EntranceDoor : MonoBehaviour
	{
		[SerializeField] Transform placeOutside;

		public void GoOut()
		{
			GameController.Instance.player.SetPositionAndRotation(placeOutside.position, placeOutside.rotation);
		}
	}
}