using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cloak.Autofac;

namespace Cloak.Web.Mvc.Autofac
{
	public class MvcModule : BuilderModule
	{
		public MvcModule(GlobalFilterCollection filters, RouteCollection routes)
		{
			RegisterModule<AreaModule>();
			RegisterModule(new ErrorModule(filters));
			RegisterModule(new RoutingModule(routes));
		}
	}
}