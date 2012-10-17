using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;

namespace Cloak.Autofac
{
	/// <summary>
	/// The composition of an application composed with an Autofac container
	/// </summary>
	public sealed class CompositionRoot : IDisposable
	{
		private readonly IModule _module;

		/// <summary>
		/// Initializes an application composition with the specified module
		/// </summary>
		/// <param name="module">The module which provides the application's composition</param>
		public CompositionRoot(IModule module)
		{
			Contract.Requires(module != null);

			_module = module;
		}

		#region IDisposable
		/// <summary>
		/// Disposes of <see cref="Container"/>
		/// </summary>
		public void Dispose()
		{
			if(Container != null)
			{
				Container.Dispose();

				Container = null;
			}
		}
		#endregion

		/// <summary>
		/// Gets the container configured by this application composition
		/// </summary>
		public IContainer Container { get; private set; }

		/// <summary>
		/// Initializes <see cref="Container"/>
		/// </summary>
		public void Compose()
		{
			var builder = new ContainerBuilder();

			builder.RegisterModule(_module);

			Container = builder.Build();
		}
	}

	/// <summary>
	/// The composition of an application composed with an Autofac container
	/// </summary>
	/// <typeparam name="TModule">The type of module which configures the container</typeparam>
	public sealed class CompositionRoot<TModule> : IDisposable where TModule : IModule, new()
	{
		#region IDisposable
		/// <summary>
		/// Disposes of <see cref="Container"/>
		/// </summary>
		public void Dispose()
		{
			if(Container != null)
			{
				Container.Dispose();

				Container = null;
			}
		}
		#endregion

		/// <summary>
		/// Gets the container configured by this application composition
		/// </summary>
		public IContainer Container { get; private set; }

		/// <summary>
		/// Initializes <see cref="Container"/>
		/// </summary>
		public void Compose()
		{
			var builder = new ContainerBuilder();

			builder.RegisterModule<TModule>();

			Container = builder.Build();
		}
	}
}