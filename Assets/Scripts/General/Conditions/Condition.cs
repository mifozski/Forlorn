using UnityEngine;

namespace Forlorn
{
	public class Condition : ScriptableObject
	{
		public string description;
		public bool satisfied;
		public int hash;
	}
}