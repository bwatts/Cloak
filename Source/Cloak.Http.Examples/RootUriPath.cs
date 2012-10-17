using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Cloak.Http
{
	public class RootUriPath
	{
		[Scenario]
		public void Get(UriPath root)
		{
			"When getting the root URI path".When(() => root = UriPath.Root);

			"Its base path is null".Then(() => root.BasePath.Should().Be(null));
			"Its value is empty".Then(() => root.Value.Should().Be(""));
		}

		[Scenario]
		public void ThenSeparator(UriPath path)
		{
			"When getting the root URI path followed by a separator".When(() => path = UriPath.Root.Then(UriPath.Separator));

			"Its base path is the root".Then(() => path.BasePath.Should().Be(UriPath.Root));
			"Its value is empty".Then(() => path.Value.Should().BeEmpty());
		}

		[Scenario]
		public void ThenText(UriPath path)
		{
			"When getting the root URI path followed by text".When(() => path = UriPath.Root.Then("text"));

			"Its base path is the root".Then(() => path.BasePath.Should().Be(UriPath.Root));
			"Its value is the specified next part".Then(() => path.Value.Should().Be("text"));
		}

		[Scenario]
		public void ThenParameter(UriPath path)
		{
			"When getting the root URI path follow by a parameter".When(() => path = UriPath.Root.ThenParameter("p"));

			"Its base path is the root".Then(() => path.BasePath.Should().Be(UriPath.Root));
			"Its value is the specified parameter name in curly braces".Then(() => path.Value.Should().Be("{p}"));
		}

		[Scenario]
		public void ThenObject(UriPath path)
		{
			"When getting the root URI path followed by an object".When(() => path = UriPath.Root.Then(1));

			"Its base path is the root".Then(() => path.BasePath.Should().Be(UriPath.Root));
			"Its has the specified object's text".Then(() => path.Value.Should().Be("1"));
		}

		[Scenario]
		public void ThenFormattedObject(NumberFormatInfo formatInfo, UriPath path)
		{
			"Given a numeric format which specifies a custom negative operator".Given(() =>
			{
				formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();

				formatInfo.NegativeSign = "NEGATIVE";
			});

			"When getting the root URI path followed by a formatted object".When(() => path = UriPath.Root.Then(-1, formatInfo));

			"Its base path is the root".Then(() => path.BasePath.Should().Be(UriPath.Root));
			"Its has the specified object's formatted text".Then(() => path.Value.Should().Be("NEGATIVE1"));
		}
	}
}