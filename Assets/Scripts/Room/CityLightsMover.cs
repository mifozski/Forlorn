using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightsMover : MonoBehaviour {

	Transform camera;

	// Use this for initialization
	void Start () {
		camera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(camera.position.x, transform.position.y, transform.position.z);
	}
}
