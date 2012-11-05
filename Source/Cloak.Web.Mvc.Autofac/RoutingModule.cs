using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;

namespace Cloak.Web.Mvc.Autofac
{
	public class RoutingModule : BuilderModule
	{
		public RoutingModule(RouteCollection routes)
		{
			Contract.Requires(routes != null);

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		}
	}
}