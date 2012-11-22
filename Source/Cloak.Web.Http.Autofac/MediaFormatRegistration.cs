using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Cloak.Http.Media;
using Cloak.Web.Http.Autofac;

namespace Cloak.Web.Http.Autofac
{
	public static class MediaFormatRegistration
	{
		public static void RegisterMediaFormat<T>(this HttpConfiguration httpSettings, ContainerBuilder builder) where T : MediaFormat, new()
		{
			Contract.Requires(httpSettings != null);
			Contract.Requires(builder != null);

			var format = new T();

			builder.RegisterInstance(format).As<MediaFormat>();

			httpSettings.Formatters.Add(format);
		}
	}
}