using UnityEngine;
using System;
using System.Collections.Generic;
using DynamicExpresso;

using Forlorn.Core.Variables;

namespace Forlorn.Core.ConditionSystem
{
	public delegate void SetVar(string name, int value);
	public delegate void CallReaction(string reaction);
	public class ExpressionEvaluator
	{
		public SetVar HandleSetVariable;
		public CallReaction HandleCallReaction
		{
			set
			{
				interpreter.SetFunction("call", value);
			}
		}

		private Interpreter interpreter = new Interpreter();

		public void Init(List<Variable> variables)
		{
			foreach (Variable variable in variables)
			{
				interpreter.SetVariable(variable.name, variable.value, typeof(int));
				Debug.Log($"Added \"{variable.name}\" variable with value: {variable.value}");
			}

			SetVar set = (n, v) =>
			{
				interpreter.SetVariable(n, v, typeof(int));
				HandleSetVariable(n, v);
			};
			interpreter.SetFunction("set", set);
		}

		public delegate void Pow(string name, int value);
		public bool Evaluate(string expression)
		{
			// Debug.Log($"Evaluating \"{expression}\"");

			var detectedIdentifiers = interpreter.DetectIdentifiers(expression);

			var result = interpreter.Eval(expression);
			if (result != null)
			{
				return (bool)result;
			}
			else
			{
				return false;
			}
		}
	}
}