using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightsMover : MonoBehaviour
{
	Transform mainCamera;

	// Use this for initialization
	void Start()
	{
		mainCamera = Camera.main.transform;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(mainCamera.position.x, transform.position.y, transform.position.z);
	}
}
