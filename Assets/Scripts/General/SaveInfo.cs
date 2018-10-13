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
	public class GameStateLoaderSaver
	{
		private static List<GameState> savedGames = new List<GameState>();

		public void Save()
		{
			savedGames.Add(GameState.current);
			BinaryFormatter bf = new BinaryFormatter();

			SurrogateSelector ss = new SurrogateSelector();
			ss.AddSurrogate(typeof(Vector3),
							new StreamingContext(StreamingContextStates.All),
							new Vector3SerializationSurrogate());
			ss.AddSurrogate(typeof(Quaternion),
							new StreamingContext(StreamingContextStates.All),
							new QuaternionSerializationSurrogate());
			bf.SurrogateSelector = ss;

			FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
			Debug.Log(Application.persistentDataPath + "/savedGames.gd");
			bf.Serialize(file, savedGames);
			Debug.Log("Data written to " + Application.persistentDataPath + "/savedGames.gd" + " @ " + DateTime.Now.ToShortTimeString());
			file.Close();

			Debug.Log($"Saved game: {JsonUtility.ToJson(GameState.current)}");
		}

		public bool Load()
		{
			savedGames.Clear();

			Debug.Log(Application.persistentDataPath + "/savedGames.gd");

			BinaryFormatter bf = new BinaryFormatter();

			SurrogateSelector ss = new SurrogateSelector();
			ss.AddSurrogate(typeof(Vector3),
							new StreamingContext(StreamingContextStates.All),
							new Vector3SerializationSurrogate());
			ss.AddSurrogate(typeof(Quaternion),
							new StreamingContext(StreamingContextStates.All),
							new QuaternionSerializationSurrogate());
			bf.SurrogateSelector = ss;

			FileStream file = null;
			try
			{
				file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
				savedGames = (List<GameState>)bf.Deserialize(file);
			}
			catch
			{
				return false;
			}
			finally
			{
				file?.Close();
			}

			GameState.current = savedGames[savedGames.Count - 1];
			GameState.current.loaded = true;

			Debug.Log($"Loaded game: {JsonUtility.ToJson(GameState.current)}");

			return true;
		}
	}
}