using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Cloak.NUnit
{
	/// <summary>
	/// Base implementation of NUnit test fixtures written in the Given-When-Then style
	/// </summary>
	[TestFixture]
	public abstract class Behavior
	{
		/// <summary>
		/// Initializes this text fixture by executing Given and When
		/// </summary>
		[TestFixtureSetUp]
		public void SetUp()
		{
			Given();

			When();
		}

		/// <summary>
		/// When implemented in a derived class, sets up the state for the tested action
		/// </summary>
		protected abstract void Given();

		/// <summary>
		/// When implemented in a derived class, performs the tested action
		/// </summary>
		protected abstract void When();
	}
}