using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak
{
	public static class Params
	{
		public static IEnumerable<T> Of<T>(params T[] values)
		{
			return values;
		}
	}
}