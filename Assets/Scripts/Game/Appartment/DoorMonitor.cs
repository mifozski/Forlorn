using UnityEngine;

namespace Forlorn.Game
{
	public class DoorMonitor : MonoBehaviour
	{
		[SerializeField] AudioSource audioSource;
		[SerializeField] Transform screen;

		bool isOn = false;

		Material screenMaterial;
		string albedoColorMapParam = "_MainTex";
		string colorParam = "_Color";
		string emissiveColorParam = "_EmissiveColor";
		string emissiveColorMapParam = "_EmissiveColorMap";

		Texture viewerTexture;

		protected InteractiveMixin interactive;

		void Start()
		{
			screenMaterial = Utils.FindEmissiveMaterial(screen);
			viewerTexture = screenMaterial.GetTexture(albedoColorMapParam);
			interactive = GetComponent<InteractiveMixin>();
		}

		public void ToggleMonitor()
		{
			audioSource.Play();
			isOn = !isOn;

			UpdateState();
		}

		void UpdateState()
		{
			screenMaterial.SetColor("_BaseColor", isOn ? Color.white : Color.black);
			screenMaterial.SetTexture("_BaseColorMap", isOn ? viewerTexture : null);
			screenMaterial.SetTexture("_EmissiveColorMap", isOn ? viewerTexture : null);
			screenMaterial.SetColor("_EmissiveColor", isOn ? Color.white : Color.black);

			interactive.onHoverSubtitles = isOn ? "Turn off" : "Turn on";
		}
	}
}
