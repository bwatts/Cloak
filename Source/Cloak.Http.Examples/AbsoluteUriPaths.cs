using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xbehave;

namespace Cloak.Http
{
	public class AbsoluteUriPaths
	{
		[Scenario]
		public void Separator()
		{
			var baseUri = default(Uri);
			var absoluteUri = default(Uri);

			"Given a base URI".Given(() => baseUri = new Uri("http://localhost"));

			"When getting an absolute URI from the root followed by a separator".When(() => absoluteUri = UriPath.Root.Then(UriPath.Separator).ToAbsoluteUri(baseUri));

			"It is the base URI followed by a separator".Then(() => absoluteUri.Should().Be(new Uri("http://localhost/")));
		}

		[Scenario]
		public void Text()
		{
			var baseUri = default(Uri);
			var absoluteUri = default(Uri);

			"Given a base URI".Given(() => baseUri = new Uri("http://localhost"));

			"When getting an absolute URI from the root followed by text".When(() => absoluteUri = UriPath.Root.Then("text").ToAbsoluteUri(baseUri));

			"It is the base URI followed by the text".Then(() => absoluteUri.Should().Be(new Uri("http://localhost/text")));
		}

		[Scenario]
		public void Paramter()
		{
			var baseUri = default(Uri);
			var absoluteUri = default(Uri);

			"Given a base URI".Given(() => baseUri = new Uri("http://localhost"));

			"When getting an absolute URI from the root followed by a parameter".When(() => absoluteUri = UriPath.Root.ThenParameter("p").ToAbsoluteUri(baseUri));

			"It is the base URI followed by the specified parameter name in curly braces".Then(() => absoluteUri.Should().Be(new Uri("http://localhost/{p}")));
		}

		[Scenario]
		public void Object()
		{
			var baseUri = default(Uri);
			var absoluteUri = default(Uri);

			"Given a base URI".Given(() => baseUri = new Uri("http://localhost"));

			"When getting an absolute URI from the root followed by an object".When(() => absoluteUri = UriPath.Root.Then(1).ToAbsoluteUri(baseUri));

			"It is the base URI followed by the specified object's text".Then(() => absoluteUri.Should().Be(new Uri("http://localhost/1")));
		}

		[Scenario]
		public void FormattedObject()
		{
			var formatInfo = default(NumberFormatInfo);
			var baseUri = default(Uri);
			var absoluteUri = default(Uri);

			"Given a base URI".Given(() => baseUri = new Uri("http://localhost"));
			"And a numeric format which specifies a custom negative operator".And(() =>
			{
				formatInfo = (NumberFormatInfo) NumberFormatInfo.CurrentInfo.Clone();

				formatInfo.NegativeSign = "NEGATIVE";
			});

			"When getting an absolute URI from the root followed by a formatted object".When(() => absoluteUri = UriPath.Root.Then(-1, formatInfo).ToAbsoluteUri(baseUri));

			"It is the base URI followed by the specified object's formatted text".Then(() => absoluteUri.Should().Be(new Uri("http://localhost/NEGATIVE1")));
		}
	}
}