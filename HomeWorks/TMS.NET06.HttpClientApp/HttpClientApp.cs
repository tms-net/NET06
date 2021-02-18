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
        private readonly Dictionary<Uri, string> _responseBodyCache;
        private readonly Dictionary<Uri, string> _etagCache;

        internal HttpClientApp(HttpMessageHandler handler)
		{
			_httpClient = new HttpClient(handler);
			_responseBodyCache = new Dictionary<Uri, string>();
			_etagCache = new Dictionary<Uri, string>();
		}

		public async Task<string> GetContentAsync(Uri uri)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			if (_etagCache.ContainsKey(uri))
            {
				request.Headers.IfNoneMatch.Add(
					new EntityTagHeaderValue(_etagCache[uri]));
            }
			var response = await _httpClient.SendAsync(request);

			if (response.StatusCode == HttpStatusCode.NotModified &&
				_responseBodyCache.ContainsKey(uri))
            {
				return _responseBodyCache[uri];
            }

			var responseBody = await response.Content.ReadAsStringAsync();

			if (!string.IsNullOrEmpty(response.Headers.ETag.Tag))
			{
				_responseBodyCache[uri] = responseBody;
				_etagCache[uri] = response.Headers.ETag.Tag;
			}

			return responseBody;
		}
	}
}
