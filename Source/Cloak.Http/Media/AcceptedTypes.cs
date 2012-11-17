using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Http.Media
{
	public class AcceptedTypes : IReadOnlyList<Type>
	{
		private readonly IList<Type> _types;

		public AcceptedTypes(IList<Type> types)
		{
			Contract.Requires(types != null);

			_types = types;
		}

		public AcceptedTypes(params Type[] types) : this(types as IList<Type>)
		{}

		public Type this[int index]
		{
			get { return _types[index]; }
		}

		public int Count
		{
			get { return _types.Count; }
		}

		public IEnumerator<Type> GetEnumerator()
		{
			return _types.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<MediaFormat> GetAcceptedMediaFormats(MediaFormats allMediaFormats)
		{
			return
				from type in _types
				from mediaFormat in allMediaFormats
				where mediaFormat.CanReadType(type)
				select mediaFormat;
		}
	}
}