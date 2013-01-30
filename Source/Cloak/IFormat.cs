using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak
{
	public interface IFormat<T> : IWriter<T>, IReader<T>
	{

	}
}