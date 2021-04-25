using UnityEngine;

namespace Forlorn
{
	[CreateAssetMenu]
	public class ReactionPlayerSO : ScriptableObject
	{
		AudioPlayer _player;

		public void SetPlayer(AudioPlayer player)
		{
			_player = player;
		}

		public void Play2D(AudioClip clip)
		{
			_player.Play(clip);
		}

		public void TransferPlayerTo(Transform target)
		{
			PlayerController.Player.transform.SetPositionAndRotation(target.position, target.rotation);
		}
	}
}
