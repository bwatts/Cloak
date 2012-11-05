using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cloak.Http.Media;
using FluentAssertions;
using Xunit;

namespace Cloak.Http
{
	public class MediaTypes
	{
		[Fact] public void CreateWithVendor()
		{
			var mediaType = new MediaType("application/vnd.grasp.test+html");

			mediaType.Path.Value.Should().Be("vnd.grasp.test+html");
			mediaType.Path.Vendor.Should().Be("grasp");
			mediaType.Path.Media.Should().Be("test");
			mediaType.Path.Format.Should().Be(MediaPath.HtmlFormat);
		}

		[Fact] public void CreateWithoutVendor()
		{
			var mediaType = new MediaType("application/json");

			mediaType.Path.Should().Be(MediaPath.Unspecified);
		}
	}
}