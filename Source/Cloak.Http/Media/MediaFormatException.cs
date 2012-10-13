using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	[Serializable]
	public class MediaFormatException : Exception
	{
		public MediaFormatException()
		{}

		public MediaFormatException(string message) : base(message)
		{}

		public MediaFormatException(string message, Exception inner) : base(message, inner)
		{}

		protected MediaFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{}
	}
}