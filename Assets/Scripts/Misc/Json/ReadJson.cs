using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReader : MonoBehaviour {

	private string jsonString;
	// private JsonData itemData;

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/JSONs/save.json");
		// itemData = JsonMapper.ToObject(jsonString);

		// Debug.Log(itemData["Save"]["stage"]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
