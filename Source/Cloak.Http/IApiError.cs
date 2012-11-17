using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http
{
	public interface IApiError
	{
		string Message { get; }

		long Code { get; }
	}
}