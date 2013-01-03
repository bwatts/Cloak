using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloak.Wpf.Behaviors
{
	/// <summary>
	/// Describes a behavior which can be attached to a host object
	/// </summary>
	public interface IBehavior
	{
		/// <summary>
		/// Determines whether this behavior is applicable to the host object
		/// </summary>
		/// <returns>Whether this behavior is applicable to the host object</returns>
		bool IsApplicable();

		/// <summary>
		/// Attaches this behavior to the host object
		/// </summary>
		void Attach();

		/// <summary>
		/// Detaches this behavior from the host object
		/// </summary>
		void Detach();

		/// <summary>
		/// Synchronizes the behavior with the current state of the host object
		/// </summary>
		void Update();
	}
}