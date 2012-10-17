using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Cloak.Http.Media
{
	public class MediaTypes
	{
		[Scenario]
		public void CreateWithVendor(MediaType mediaType)
		{
			"When creating a media type with a vendor".When(() => mediaType = new MediaType("application/vnd.grasp.test+html"));

			"It has the specified media path".Then(() => mediaType.Path.Value.Should().Be("vnd.grasp.test+html"));
			"it has the specified vendor".Then(() => mediaType.Path.Vendor.Should().Be("grasp"));
			"it has the specified media".Then(() => mediaType.Path.Media.Should().Be("test"));
			"it has the specified format".Then(() => mediaType.Path.Format.Should().Be(MediaPath.HtmlFormat));
		}

		[Scenario]
		public void CreateWithoutVendor(MediaType mediaType)
		{
			"When creating a media type with a vendor".When(() => mediaType = new MediaType("application/json"));

			"It has an unspecified media path".Then(() => mediaType.Path.Should().Be(MediaPath.Unspecified));
		}
	}
}