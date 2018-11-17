using UnityEngine;

using Forlorn;

namespace Forlorn
{
	// [RequireComponent(typeof(Light), typeof(AudioSource))]
	public class SwitchableLightMixin : MonoBehaviour
	{
		private new Light light;
		private AudioSource humming;

		Animator doorAnimator;

		Material emissionMaterial;
		[SerializeField] MeshRenderer emissiveMaterialrenderer;
		[SerializeField] Color m_OriginalEmissiveMatColor;

		public bool lightIsOn
		{
			set {
				if (light != null)
				{
					light.enabled = value;
				}
				else if (emissionMaterial)
				{
					if (value)
					{
						emissionMaterial.SetColor("_EmissionColor", m_OriginalEmissiveMatColor);
					}
					else
					{
						emissionMaterial.SetColor("_EmissionColor", Color.black);
					}
					emissiveMaterialrenderer.UpdateGIMaterials();
				}
				if (humming)
					PlayHumming(value);
			}
			get {
				if (light != null)
					return light.enabled;
				else if (emissionMaterial)
					return emissionMaterial.GetColor("_EmissionColor") != Color.black;
				else
				{
					Debug.LogError("No light or emissive material found on the object");
					return true;
				}
			}
		}

		void Awake()
		{
			light = GetComponentInChildren<Light>();
			if (light == null)
			{
				emissiveMaterialrenderer = GetComponent<MeshRenderer>();
				foreach (Material mat in emissiveMaterialrenderer.materials)
				{
					Color we = mat.GetColor("_EmissionColor");
					if (mat.GetColor("_EmissionColor") != Color.black)
					{
						emissionMaterial = mat;
						m_OriginalEmissiveMatColor = emissionMaterial.GetColor("_EmissionColor");
						break;
					}
				}
			}

			humming = GetComponent<AudioSource>();
			if (humming)
			{
				humming.loop = true;
				PlayHumming(light.enabled);
			}
		}

		public bool IsOn()
		{
			return lightIsOn;
		}

		private void PlayHumming(bool play)
		{
			if (play)
				humming.Play();
			else
				humming.Stop();
		}
	}
}