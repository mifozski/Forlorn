using System.Collections.Generic;

using Forlorn;

namespace Forlorn {
interface ISerializable
{
    void Serialize();

    void Deserialize();
};

class SaveLoadController
{
    List<ISerializable> serializables = new List<ISerializable>();

    public void AddSerializableObject(ISerializable _object)
    {
        serializables.Add(_object);
    }

    void Save()
    {
        serializables.ForEach(
            obj => obj.Serialize()
        );

        // SaveLoadGame.Save();
    }

    void Load()
    {
        serializables.ForEach(
            obj => obj.Deserialize()
        );

        // SaveLoadGame.Load();
    }
}
}