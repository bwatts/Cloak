using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Cloak.Autofac;

namespace Cloak.Web.Mvc.Autofac
{
	public class MvcCompositionRoot
	{
		private readonly CompositionRoot _baseRoot;

		public MvcCompositionRoot(CompositionRoot baseRoot)
		{
			Contract.Requires(baseRoot != null);

			_baseRoot = baseRoot;
		}

		public void Compose()
		{
			_baseRoot.Compose();

			IntegrateWithMvc();
		}

		private void IntegrateWithMvc()
		{
			DependencyResolver.SetResolver(new AutofacDependencyResolver(_baseRoot.Container));
		}
	}
}