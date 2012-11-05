using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using Cloak.Autofac;

namespace Cloak.Web.Mvc.Autofac
{
	public class AreaModule : BuilderModule
	{
		public AreaModule()
		{
			AreaRegistration.RegisterAllAreas();
		}
	}
}