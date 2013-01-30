using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak
{
	public interface IWriter<in T>
	{
		Task WriteAsync(T content, Stream stream);
	}
}