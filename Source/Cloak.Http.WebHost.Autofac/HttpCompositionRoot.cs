using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Cloak.Autofac;

namespace Cloak.Http.WebHost.Autofac
{
	public class HttpCompositionRoot
	{
		private readonly CompositionRoot _baseRoot;

		public HttpCompositionRoot(CompositionRoot baseRoot)
		{
			Contract.Requires(baseRoot != null);

			_baseRoot = baseRoot;
		}

		public void Compose()
		{
			_baseRoot.Compose();

			IntegrateWithWebApi();
		}

		private void IntegrateWithWebApi()
		{
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_baseRoot.Container);
		}
	}
}