using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ReverbZoneMixin : MonoBehaviour
{
	[SerializeField] AudioReverbZone reverbZone;
	// Start is called before the first frame update
	void Start()
	{

	}

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
