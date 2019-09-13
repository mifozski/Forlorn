using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Forlorn.Core.Variables
{
	[Serializable]
	public struct Variable
	{
		[SerializeField] public string name;
		[SerializeField] public int value;
	}

	[CreateAssetMenu]
	public class VariableCollection : ScriptableObject
	{
		public List<Variable> Variables = new List<Variable>();

		public void GetVariables(out List<Variable> variables)
		{
			variables = this.Variables;
		}

		public void SetVariable(string name, int value)
		{
			if (Variables.Where(v => v.name == name).ToArray().Length == 0)
			{
				Debug.LogError($"Variable named \"{name}\" does not exist in the variable collection");
				return;
			}

			var index = Variables.FindIndex(0, v => v.name == name);
			Variables[index] = new Variable { name = name, value = value };
		}
	}
}