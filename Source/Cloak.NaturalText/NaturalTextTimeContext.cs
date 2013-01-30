using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloak.Time;
using DateTimeExtensions;

namespace Cloak.NaturalText
{
	public sealed class NaturalTextTimeContext : ITimeContext
	{
		private readonly ITimeContext _baseContext;

		public NaturalTextTimeContext(ITimeContext baseContext)
		{
			Contract.Requires(baseContext != null);

			_baseContext = baseContext;
		}

		public DateTime Now
		{
			get { return _baseContext.Now; }
		}

		public RelativeTime NowRelativeTo(DateTime referencePoint)
		{
			return new RelativeTime(Now, referencePoint, Now.ToNaturalText(referencePoint, round: true));
		}
	}
}