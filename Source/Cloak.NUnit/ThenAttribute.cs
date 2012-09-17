using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Cloak.NUnit
{
	/// <summary>
	/// Indicates an assertion on the result of a tested action
	/// </summary>
	[Obsolete(@"Due to changes in NUnit 2.6, this attribute, when declared in a separate assembly (such as Cloak.NUnit), is no longer recognized by test runners.

Workaround: Replace usages of this attribute with NUnit.Framework.TestAttribute directly.",
		error: true)]
	public class ThenAttribute : TestAttribute
	{
		
	}
}