using System.Runtime.Serialization;
using UnityEngine;

sealed class Vector3SerializationSurrogate : ISerializationSurrogate
{
	// Method called to serialize a Vector3 object
	public void GetObjectData(System.Object obj,
							  SerializationInfo info, StreamingContext context)
	{
		Vector3 v3 = (Vector3) obj;
		info.AddValue("x", v3.x);
		info.AddValue("y", v3.y);
		info.AddValue("z", v3.z);
	}

	// Method called to deserialize a Vector3 object
	public System.Object SetObjectData(System.Object obj,
									   SerializationInfo info, StreamingContext context,
									   ISurrogateSelector selector)
	{
		Vector3 v3 = (Vector3)obj;
		v3.x = (float)info.GetValue("x", typeof(float));
		v3.y = (float)info.GetValue("y", typeof(float));
		v3.z = (float)info.GetValue("z", typeof(float));
		obj = v3;
		return obj;
	}
}

sealed class QuaternionSerializationSurrogate : ISerializationSurrogate
{
	// Method called to serialize a Vector3 object
	public void GetObjectData(System.Object obj,
							  SerializationInfo info, StreamingContext context)
	{
		Quaternion quat = (Quaternion)obj;
		info.AddValue("x", quat.x);
		info.AddValue("y", quat.y);
		info.AddValue("z", quat.z);
		info.AddValue("w", quat.w);
	}

	// Method called to deserialize a Vector3 object
	public System.Object SetObjectData(System.Object obj,
									   SerializationInfo info, StreamingContext context,
									   ISurrogateSelector selector)
	{
		Quaternion quat = (Quaternion) obj;
		quat.x = (float)info.GetValue("x", typeof(float));
		quat.y = (float)info.GetValue("y", typeof(float));
		quat.z = (float)info.GetValue("z", typeof(float));
		quat.w = (float)info.GetValue("w", typeof(float));
		obj = quat;
		return obj;   // Formatters ignore this return value //Seems to have been fixed!
	}
}