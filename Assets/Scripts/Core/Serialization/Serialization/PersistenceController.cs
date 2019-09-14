﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEditor;

using Project;

namespace Serialization
{
	[Serializable]
	public class PersistentData
	{
		public Dictionary<string, object> genericObjects;
		public Dictionary<string, PersistentObject> precreatedGameObjects;
	}

	public class PersistenceController : MonoBehaviour
	{
		[SerializeField] private PrefabMap _prefabMap;

		Serializer serializer = new Serializer();

		private PersistentData m_PersistentData = new PersistentData
		{
			genericObjects = new Dictionary<string, object>(),
			precreatedGameObjects = new Dictionary<string, PersistentObject>(),
		};

		static private PersistenceController _instance;
		private static bool applicationIsQuitting = false;

		static PersistenceController Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					return null;
				}
				if (_instance == null)
				{
					_instance = FindObjectOfType<PersistenceController>();
				}
				return _instance;
			}
		}

		public PersistentData GetDeserializedData()
		{
			return m_PersistentData;
		}

		static public void RegisterPersistentObject(PersistentUid uid, PersistentObject persistentObject)
		{
			if (Instance)
			{
				Instance.m_PersistentData.precreatedGameObjects.Add(uid, persistentObject);
				Debug.Log($"Registering created object: {persistentObject.name} with uid: {uid}");
			}
		}

		static public void UnregisterPersistentObject(PersistentUid uid)
		{
			if (Instance)
				Instance.m_PersistentData.precreatedGameObjects.Remove(uid);
		}

		static public PersistentObject GetPrecreatedPersistentObject(PersistentUid uid)
		{
			PersistentObject persistentObject;
			Instance.m_PersistentData.precreatedGameObjects.TryGetValue(uid, out persistentObject);
			return persistentObject;
		}

		public static void RegisterPrefab(PersistentObject prefab)
		{
			if (Instance)
			{
				Instance.AddPrefab(prefab);
			}
		}

		public static void UnregisterPrefab(PersistentObject prefab)
		{
			if (Instance)
			{
				Instance.RemovePrefab(prefab);
			}
		}

		public static void OverridePrefabs(List<PersistentObject> prefabs)
		{
			if (Instance)
			{
				Instance.SetPrefabs(prefabs);
			}
		}

		public static PersistentObject GetRegisteredPrefab(string uid)
		{
			if (Instance)
			{
				return Instance.GetPrefab(uid);
			}
			return null;
		}

		private void AddPrefab(PersistentObject prefab)
		{
			if (_prefabMap == null)
				CreatePrefabMap();

			_prefabMap.Add(prefab);
		}

		private void RemovePrefab(PersistentObject prefab)
		{
			if (_prefabMap == null)
			{
				CreatePrefabMap();
				return;
			}

			_prefabMap.Remove(prefab);
		}

		private void SetPrefabs(List<PersistentObject> prefabs)
		{
			if (_prefabMap == null)
			{
				CreatePrefabMap();
			}

			_prefabMap.Set(prefabs);
		}

		private PersistentObject GetPrefab(string uid)
		{
			if (_prefabMap == null)
				return null;

			return _prefabMap.GetPrefab(uid);
		}

		private void CreatePrefabMap()
		{
#if UNITY_EDITOR
			_prefabMap = ScriptableObject.CreateInstance<PrefabMap>();

			string path = "Assets";
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(PrefabMap).ToString() + ".asset");

			AssetDatabase.CreateAsset(_prefabMap, assetPathAndName);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
#endif
		}

		public void Serialize()
		{
			string saveFilePath = Application.persistentDataPath + "/" + "Save.json";

			var serializer = new Serializer();

			FileStream file = File.Create(saveFilePath);
			file.SetLength(0);

			// serializer.Serialize(file, m_PrecreatedPersistentObjects);

			serializer.Serialize(file, m_PersistentData);

			// foreach (KeyValuePair<long, PrecreatedPersistentObject> entry in m_PrecreatedPersistentObjects)
			// {
			// 	serializer.Serialize(file, entry.Value);
			// }
			// foreach (PersistentObject obj in m_PersistentObjects)
			// {
			// 	serializer.Serialize(file, obj);
			// }


			Debug.Log("Data written to " + saveFilePath + " @ " + DateTime.Now.ToShortTimeString());
			file.Close();
		}

		public bool Deserialize()
		{
			// guids = AssetDatabase.FindAssets("t:ScriptObj");

			string saveFilePath = Application.persistentDataPath + "/" + "Save.json";
			Debug.Log($"Reading save file at {saveFilePath}");
			FileStream file;
			try
			{
				file = new FileStream(saveFilePath, FileMode.Open, FileAccess.Read);
			}
			catch
			{
				return false;
			}

			var serializer = new Serializer();

			m_PersistentData = serializer.Deserialize(file) as PersistentData;

			Debug.Log("Deserialized");

			return true;
		}

		class Serializer
		{
			JsonFormatter bf;
			SPSerializer serializer;

			public Serializer()
			{
				bf = new JsonFormatter();
				serializer = new SPSerializer();
				serializer.AssetBundle = ResourcesAssetBundle.Instance;
			}

			public void Serialize(Stream serializationStream, object graph)
			{
				serializer.Serialize(bf, serializationStream, graph);
			}
			public object Deserialize(Stream serializationStream)
			{
				return serializer.Deserialize(bf, serializationStream);
			}
		}

		public static void AddSerializedObject(string key, object serializedObject)
		{
			if (Instance)
			{
				Instance.m_PersistentData.genericObjects[key] = serializedObject;
			}
		}

		public static object GetSerializedObject(string key)
		{
			object serializedObject = null;
			if (Instance)
			{

				Instance.m_PersistentData.genericObjects.TryGetValue(key, out serializedObject);
			}

			return serializedObject;
		}

		public static void Save()
		{
			if (Instance)
			{
				Instance.Serialize();
			}
		}

		public static void Load()
		{
			if (Instance)
			{
				Instance.Deserialize();
			}
		}

		private void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}
}
