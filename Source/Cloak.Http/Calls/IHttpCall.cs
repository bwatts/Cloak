using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cloak.Http.Media;

namespace Cloak.Http.Calls
{
	[ContractClass(typeof(IHttpCallContract))]
	public interface IHttpCall
	{
		Task<HttpResponseMessage> SendAsync(HttpClient client, MediaFormats mediaFormats);
	}

	[ContractClass(typeof(IHttpCallContract<>))]
	public interface IHttpCall<TResult>
	{
		Task<TResult> SendAsync(HttpClient client, MediaFormats mediaFormats);
	}

	[ContractClassFor(typeof(IHttpCall))]
	internal abstract class IHttpCallContract : IHttpCall
	{
		Task<HttpResponseMessage> IHttpCall.SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			Contract.Requires(client != null);
			Contract.Requires(mediaFormats != null);
			Contract.Ensures(Contract.Result<Task<HttpResponseMessage>>() != null);

			return null;
		}
	}

	[ContractClassFor(typeof(IHttpCall<>))]
	internal abstract class IHttpCallContract<TResult> : IHttpCall<TResult>
	{
		Task<TResult> IHttpCall<TResult>.SendAsync(HttpClient client, MediaFormats mediaFormats)
		{
			Contract.Requires(client != null);
			Contract.Requires(mediaFormats != null);
			Contract.Ensures(Contract.Result<Task<TResult>>() != null);

			return null;
		}
	}
}