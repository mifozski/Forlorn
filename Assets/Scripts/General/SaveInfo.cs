using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

using Forlorn;

namespace Forlorn
{
// public class SaveLoad : MonoBehaviour
// {
// 	// public SlotLoader SlotLoader;
// 	//data is what is finally saved
// 	public Dictionary<string, int> data;

// 	void Awake()
// 	{
// 		//LoadUpgradeData();
// 		LoadData();
// 		//WARNING! data.Clear() deletes EVERYTHING
// 		//data.Clear();
// 		//SaveData();
// 	}

// 	public void LoadData()
// 	{
// 		//this loads the data
// 		data = SaveLoad.DeserializeData<Dictionary<string, int>>("PleaseWork.save");
// 	}

// 	public void SaveData()
// 	{
// 		//this saves the data
// 		SaveLoad.SerializeData(data, "PleaseWork.save");
// 	}
// 	public static void SerializeData<T>(T data, string path)
// 	{
// 		//this is just magic to save data.
// 		//if you're interested read up on serialization
// 		FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
// 		BinaryFormatter formatter = new BinaryFormatter();
// 		try
// 		{
// 			formatter.Serialize(fs, data);
// 			Debug.Log("Data written to " + path + " @ " + DateTime.Now.ToShortTimeString());
// 		}
// 		catch (SerializationException e)
// 		{
// 			Debug.LogError(e.Message);
// 		}
// 		finally
// 		{
// 			fs.Close();
// 		}
// 	}

// 	public static T DeserializeData<T>(string path)
// 	{
// 		//this is the magic that deserializes the data so we can load it
// 		T data = default(T);

// 		if (File.Exists(path))
// 		{
// 			FileStream fs = new FileStream(path, FileMode.Open);
// 			try
// 			{
// 				BinaryFormatter formatter = new BinaryFormatter();
// 				data = (T)formatter.Deserialize(fs);
// 				Debug.Log("Data read from " + path);
// 			}
// 			catch (SerializationException e)
// 			{
// 				Debug.LogError(e.Message);
// 			}
// 			finally
// 			{
// 				fs.Close();
// 			}
// 		}
// 		return data;
// 	}
// }

public static class SaveLoadGame
{
	private static List<GameState> savedGames = new List<GameState>();

	//it's static so we can call it from anywhere
	public static void Save()
	{
		SaveLoadGame.savedGames.Add(GameState.current);
		BinaryFormatter bf = new BinaryFormatter();

		SurrogateSelector ss = new SurrogateSelector();
		ss.AddSurrogate(typeof(Vector3),
						new StreamingContext(StreamingContextStates.All),
						new Vector3SerializationSurrogate());
		ss.AddSurrogate(typeof(Quaternion),
						new StreamingContext(StreamingContextStates.All),
						new QuaternionSerializationSurrogate());
		bf.SurrogateSelector = ss;

		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoadGame.savedGames);
		Debug.Log("Data written to " + Application.persistentDataPath + "/savedGames.gd" + " @ " + DateTime.Now.ToShortTimeString());
		file.Close();

		Debug.Log($"Saved game: {JsonUtility.ToJson(GameState.current)}");
	}

	public static void Load()
	{
		if (!File.Exists(Application.persistentDataPath + "/savedGames.gd"))
			return;

		BinaryFormatter bf = new BinaryFormatter();

		SurrogateSelector ss = new SurrogateSelector();
		ss.AddSurrogate(typeof(Vector3),
						new StreamingContext(StreamingContextStates.All),
						new Vector3SerializationSurrogate());
		ss.AddSurrogate(typeof(Quaternion),
						new StreamingContext(StreamingContextStates.All),
						new QuaternionSerializationSurrogate());
		bf.SurrogateSelector = ss;

		FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
		SaveLoadGame.savedGames = (List<GameState>)bf.Deserialize(file);
		file.Close();

		GameState.current = SaveLoadGame.savedGames[SaveLoadGame.savedGames.Count - 1];
		GameState.current.loaded = true;

		Debug.Log($"Loaded game: {JsonUtility.ToJson(GameState.current)}");
	}
}
}