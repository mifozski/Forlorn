using System.Runtime.Serialization;

namespace Serialization
{
	interface OnDeserializedCallback
	{
		void OnDeserialized();
	}

	interface OnSerializeCallback
	{
		void OnSerialize(ref SerializationInfo info);
		void OnDeserialize(SerializationInfo info);
	}
}