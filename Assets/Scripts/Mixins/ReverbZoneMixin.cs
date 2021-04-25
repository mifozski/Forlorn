using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ReverbZoneMixin : MonoBehaviour
{
	[SerializeField] AudioReverbZone reverbZone;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			reverbZone.enabled = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			reverbZone.enabled = false;
		}
	}
}
