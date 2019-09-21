using System;
using System.Runtime.Serialization;

using UnityEngine;

namespace Serialization
{
	[Serializable]
	public class GameObjectPersistenceToken
	{
		[NonSerialized]
		private PersistentObject persistentObject;
		[NonSerialized]
		private SerializationInfo info;
		[NonSerialized]
		private StreamingContext context;

		public void SetGameObject(PersistentObject _object)
		{
			persistentObject = _object;
		}

		public PersistentObject GetObject()
		{
			return persistentObject;
		}

		public SerializationInfo GetSerializationInfo()
		{
			return info;
		}

		public void OnSerialize(SerializationInfo info, StreamingContext context)
		{
			if (persistentObject == null) return;

			persistentObject.OnSerialize(info, context);
		}

		public void OnDeserialize(SerializationInfo _info, StreamingContext _context)
		{
			Debug.Log("OnDeserialize");
			info = _info;
			context = _context;
		}

		public void Deserialize()
		{
			persistentObject.OnDeserialize(info, context, null);
			persistentObject.OnDeserialization();
		}
	}
}