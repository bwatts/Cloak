using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http
{
	public class ApiError
	{
		public ApiError(string message = "", long code = 0)
		{
			Contract.Requires(message != null);

			Message = message;
			Code = code;
		}

		public string Message { get; private set; }

		public long Code { get; private set; }
	}
}