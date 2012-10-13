using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http
{
	[Serializable]
	public class ApiException : Exception
	{
		public ApiException(long code = 0)
		{
			Code = code;
		}

		public ApiException(string message, long code = 0) : base(message)
		{
			Code = code;
		}

		public ApiException(string message, Exception inner, long code = 0) : base(message, inner)
		{
			Code = code;
		}

		protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}

		public long Code { get; private set; }
	}
}