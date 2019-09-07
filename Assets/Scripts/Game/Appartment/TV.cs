using UnityEngine;

public class TV : MonoBehaviour
{
	[SerializeField] Light backlight;
	void Start()
	{
		backlight = GetComponentInChildren<Light>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ToggleTV()
	{
		backlight.enabled = !backlight.enabled;
	}
}
