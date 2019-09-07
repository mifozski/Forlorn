using UnityEngine;

using DynamicExpresso;

namespace Forlorn.ConditionSystem
{
	public class ExpressionEvaluator
	{
		private Interpreter interpreter = new Interpreter();

		public void Init()
		{
			// var result = interpreter.Eval("8 / 2 + 2");
			// Debug.Log($"RESULT: {result}!!!!!!!!!!!!!!!!");
		}

		public bool Evaluate(string expression)
		{
			var result = interpreter.Eval(expression);
			if (result != null)
			{
				return (bool)result;
			}
			else
			{
				Debug.LogError($"Coulnd't evaluate \"{expression}\" expression");
				return false;
			}
		}
	}
}