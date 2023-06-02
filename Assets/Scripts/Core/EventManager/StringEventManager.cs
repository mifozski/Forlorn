using UnityEngine;

namespace Forlorn.Events
{
	[System.Serializable]
	public class StringEventManager : EventManager<string>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void StaticInitialize()
		{
			Initialize();
		}
	};
}