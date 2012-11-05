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
	public class MediaPaths
	{
		[Fact] public void Create()
		{
			var path = new MediaPath("application/vnd.grasp.test+html");

			path.Value.Should().Be("vnd.grasp.test+html");
			path.Vendor.Should().Be("grasp");
			path.Media.Should().Be("test");
			path.Format.Should().Be(MediaPath.HtmlFormat);
		}

		[Fact] public void CreateWithoutMedia()
		{
			var path = new MediaPath("application/vnd.grasp+html");

			path.Value.Should().Be("vnd.grasp+html");
			path.Vendor.Should().Be("grasp");
			path.Media.Should().BeEmpty();
			path.Format.Should().Be(MediaPath.HtmlFormat);
		}

		[Fact] public void CreateWithoutFormat()
		{
			var path = new MediaPath("application/vnd.grasp.test");

			path.Value.Should().BeEmpty();
			path.Vendor.Should().BeEmpty();
			path.Media.Should().BeEmpty();
			path.Format.Should().BeEmpty();
		}

		[Fact] public void CreateWithoutMediaOrFormat()
		{
			var path = new MediaPath("application/vnd.grasp");

			path.Value.Should().BeEmpty();
			path.Vendor.Should().BeEmpty();
			path.Media.Should().BeEmpty();
			path.Format.Should().BeEmpty();
		}

		[Fact] public void CreateWithoutVendor()
		{
			var path = new MediaPath("application/json");

			path.Value.Should().BeEmpty();
			path.Vendor.Should().BeEmpty();
			path.Media.Should().BeEmpty();
			path.Format.Should().BeEmpty();
		}
	}
}