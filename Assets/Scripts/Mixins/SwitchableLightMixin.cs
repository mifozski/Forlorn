using UnityEngine;

namespace Forlorn
{
	// [RequireComponent(typeof(Light), typeof(AudioSource))]
	public class SwitchableLightMixin : MonoBehaviour
	{
		private new Light light;
		private AudioSource humming;

		Material emissionMaterial;
		MeshRenderer emissiveMaterialRenderer;
		Color m_OriginalEmissiveMatColor;

		string emissiveColorParam = "_EmissionColor";

		public bool lightIsOn
		{
			set
			{
				if (light != null)
				{
					light.enabled = value;
					light.intensity = light.enabled ? _originalIntensity : 0.0f;
				}
				else if (emissionMaterial)
				{
					if (value)
					{
						emissionMaterial.SetColor(emissiveColorParam, m_OriginalEmissiveMatColor);
					}
					else
					{
						emissionMaterial.SetColor(emissiveColorParam, Color.black);
					}
					emissiveMaterialRenderer.UpdateGIMaterials();
				}
				if (humming)
					PlayHumming(value);
			}
			get
			{
				if (light != null)
				{
					return light.enabled;
				}
				else if (emissionMaterial)
					return emissionMaterial.GetColor(emissiveColorParam) != Color.black;
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
				_originalIntensity = light.intensity;

				// Try to get the emissive material
				emissiveMaterialRenderer = GetComponent<MeshRenderer>();
				foreach (Material mat in emissiveMaterialRenderer.materials)
				{
					if (mat.GetColor(emissiveColorParam) != Color.black)
					{
						emissionMaterial = mat;
						m_OriginalEmissiveMatColor = emissionMaterial.GetColor(emissiveColorParam);
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

		private void PlayHumming(bool play)
		{
			if (play)
			{
				humming.Play();
			}
			else
			{
				humming.Stop();
			}
		}

		private float _originalIntensity;
	}
}