using System;
using System.Collections.Generic;

namespace Forlorn
{
	public static class ExtensionMethods
	{
		public static IEnumerable<T> SetProperty<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var item in list)
			{
				action.Invoke(item);
			}
			return list;
		}
	}
}