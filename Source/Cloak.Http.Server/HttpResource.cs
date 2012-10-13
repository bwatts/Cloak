using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Cloak.Http.Server
{
	public abstract class HttpResource : ApiController
	{
		public const string Suffix = "Resource";

		public static readonly DefaultHttpControllerTypeResolver Resolver = new DefaultHttpControllerTypeResolver(IsResource);

		private static bool IsResource(Type type)
		{
			return type != null
				&& type.IsClass
				&& type.IsPublic
				&& type.Name.EndsWith(Suffix)
				&& !type.IsAbstract
				&& typeof(HttpResource).IsAssignableFrom(type);
		}
	}
}