using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo("TMS.NET06.HttpClientApp.Tests")]
namespace TMS.NET06.HttpClientApp
{
	public class HttpClientApp
	{
		private readonly HttpClient _httpClient;

		internal HttpClientApp(HttpMessageHandler handler)
		{
			_httpClient = new HttpClient(handler);
		}

		public Task<string> GetContentAsync(Uri uri)
		{
			return _httpClient.GetStringAsync(uri);
		}
	}
}
