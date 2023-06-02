using UnityEngine;
using UnityEngine.SceneManagement;

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
			Debug.LogError("Transfering");
			PlayerController.player.TransferTo(target);
		}

		public void SetSceneActive(string sceneName)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
		}

		public void SetRSEnvironmentLightingIntensityMultiplier(float intensity)
		{
			RenderSettings.ambientIntensity = intensity;
		}
		public void SetRSFogEndDistance(float distance)
		{
			RenderSettings.fogEndDistance = distance;
		}
	}
}
