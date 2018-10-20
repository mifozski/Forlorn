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

		private string saveFilename = "savedGames.gd";

		Serializer serializer = new Serializer();

		public void Save()
		{
			string saveFilePath = Application.persistentDataPath + "/" + saveFilename;

			savedGames.Add(GameState.current);

			FileStream file = File.Create(saveFilePath);
			Debug.Log(saveFilePath);

			serializer.Serialize(file, savedGames);
			Debug.Log("Data written to " + saveFilePath + " @ " + DateTime.Now.ToShortTimeString());

			file.Close();

			Debug.Log($"Saved game: {JsonUtility.ToJson(GameState.current)}");
		}

		public bool Load()
		{
			string saveFilePath = Application.persistentDataPath + "/" + saveFilename;

			savedGames.Clear();

			Debug.Log(saveFilePath);

			FileStream file = null;
			try
			{
				file = File.Open(saveFilePath, FileMode.Open);
				savedGames = (List<GameState>)serializer.Deserialize(file);
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

			Debug.Log($"Game loaded: {JsonUtility.ToJson(GameState.current)}");

			return true;
		}

		class Serializer
		{
			BinaryFormatter bf;

			public Serializer()
			{
				bf = new BinaryFormatter();
				SurrogateSelector ss = new SurrogateSelector();
				ss.AddSurrogate(typeof(Vector3),
								new StreamingContext(StreamingContextStates.All),
								new Vector3SerializationSurrogate());
				ss.AddSurrogate(typeof(Quaternion),
								new StreamingContext(StreamingContextStates.All),
								new QuaternionSerializationSurrogate());
				bf.SurrogateSelector = ss;
			}

			public void Serialize(Stream serializationStream, object graph)
			{
				bf.Serialize(serializationStream, graph);
			}
			public object Deserialize(Stream serializationStream)
			{
				return bf.Deserialize(serializationStream);
			}
		}
	}
}