using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Cloak.Http.Media
{
	public class MediaPaths
	{
		[Scenario]
		public void Create()
		{
			var path = default(MediaPath);

			"When creating a media path".When(() => path = new MediaPath("application/vnd.grasp.test+html"));

			"It has the specified value".Then(() => path.Value.Should().Be("vnd.grasp.test+html"));
			"it has the specified vendor".Then(() => path.Vendor.Should().Be("grasp"));
			"it has the specified media".Then(() => path.Media.Should().Be("test"));
			"it has the specified format".Then(() => path.Format.Should().Be(MediaPath.HtmlFormat));
		}

		[Scenario]
		public void CreateWithoutMedia()
		{
			var path = default(MediaPath);

			"When creating a media path without media".When(() => path = new MediaPath("application/vnd.grasp+html"));

			"It has the specified value".Then(() => path.Value.Should().Be("vnd.grasp+html"));
			"it has the specified vendor".Then(() => path.Vendor.Should().Be("grasp"));
			"it has an empty media".Then(() => path.Media.Should().Be(""));
			"it has the specified format".Then(() => path.Format.Should().Be(MediaPath.HtmlFormat));
		}

		[Scenario]
		public void CreateWithoutFormat()
		{
			var path = default(MediaPath);

			"When creating a media path without a format".When(() => path = new MediaPath("application/vnd.grasp.test"));

			"It has an empty value".Then(() => path.Value.Should().Be(""));
			"it has an empty vendor".Then(() => path.Vendor.Should().Be(""));
			"it has an empty media".Then(() => path.Media.Should().Be(""));
			"it has an empty format".Then(() => path.Format.Should().Be(""));
		}

		[Scenario]
		public void CreateWithoutMediaOrFormat()
		{
			var path = default(MediaPath);

			"When creating a media path without media or format".When(() => path = new MediaPath("application/vnd.grasp"));

			"It has an empty value".Then(() => path.Value.Should().Be(""));
			"it has an empty vendor".Then(() => path.Vendor.Should().Be(""));
			"it has an empty media".Then(() => path.Media.Should().Be(""));
			"it has an empty format".Then(() => path.Format.Should().Be(""));
		}

		[Scenario]
		public void CreateWithoutVendor()
		{
			var path = default(MediaPath);

			"When creating a media path a vendor".When(() => path = new MediaPath("application/json"));

			"It has an empty value".Then(() => path.Value.Should().Be(""));
			"it has an empty vendor".Then(() => path.Vendor.Should().Be(""));
			"it has an empty media".Then(() => path.Media.Should().Be(""));
			"it has an empty format".Then(() => path.Format.Should().Be(""));
		}
	}
}