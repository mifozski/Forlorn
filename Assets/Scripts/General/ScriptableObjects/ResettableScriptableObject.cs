using UnityEngine;

namespace Forlorn
{
	public abstract class ResettableScriptableObject : ScriptableObject
	{
		public abstract void Reset();
	}
}
