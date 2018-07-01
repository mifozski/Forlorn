using UnityEngine;
using UnityEngine.Events;

namespace Forlorn
{

[System.Serializable]
public class InteractiveObjectTypeEvent : UnityEvent<InteractiveObjectType>
{
}

[System.Serializable]
public class IntEvent : UnityEvent<int>
{
}

[System.Serializable]
public class FloatEvent : UnityEvent<float>
{
}

[System.Serializable]
public class StringEvent : UnityEvent<string>
{
}
}