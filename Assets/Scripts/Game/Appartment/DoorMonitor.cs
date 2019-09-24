using UnityEngine;

public class DoorMonitor : MonoBehaviour
{
	[SerializeField] Material screenMaterial;

	bool isOn = false;

	string albedoColorMapParam = "_MainTex";
	string emissiveColorParam = "_EmissiveColor";
	string emissiveColorMapParam = "_EmissiveColorMap";

	Texture viewerTexture;

	void Start()
	{
		viewerTexture = screenMaterial.GetTexture(albedoColorMapParam);
	}

	public void ToggleMonitor()
	{
		isOn = !isOn;
		screenMaterial.SetColor(emissiveColorParam, isOn ? Color.white : Color.black);
		screenMaterial.SetTexture(emissiveColorMapParam, isOn ? viewerTexture : null);
		screenMaterial.SetTexture(albedoColorMapParam, isOn ? viewerTexture : null);
	}
}
