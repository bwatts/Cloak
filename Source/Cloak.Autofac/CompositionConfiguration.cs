using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Cloak;

namespace Cloak.Autofac
{
	/// <summary>
	/// Reads optional and required sections of application configuration files
	/// </summary>
	public static class CompositionConfiguration
	{
		/// <summary>
		/// Gets the section with the specified section and name
		/// </summary>
		/// <typeparam name="T">The type of section to get</typeparam>
		/// <param name="name">The name of the section to get</param>
		/// <returns>The section with the specified name, or null if the section is not specified</returns>
		public static T GetOptionalSection<T>(string name) where T : ConfigurationSection
		{
			return (T) ConfigurationManager.GetSection(name);
		}

		/// <summary>
		/// Gets the section with the specified section and name
		/// </summary>
		/// <typeparam name="T">The type of section to get</typeparam>
		/// <param name="name">The name of the section to get</param>
		/// <returns>The section with the specified name</returns>
		/// <exception cref="ConfigurationErrorsException">Thrown if the section is not specified</exception>
		public static T GetRequiredSection<T>(string name) where T : ConfigurationSection
		{
			var configuration = GetOptionalSection<T>(name);

			if(configuration == null)
			{
				throw new ConfigurationErrorsException(Resources.ConfigurationSectionNotSpecified.FormatCurrent(name));
			}

			return configuration;
		}
	}
}