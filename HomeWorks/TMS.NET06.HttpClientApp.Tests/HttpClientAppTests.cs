using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace TMS.NET06.HttpClientApp.Tests
{
	public class HttpClientAppTests
	{
		[SetUp]
		public void Setup()
		{
			//https://en.wikipedia.org/wiki/HTTP_ETag
		}

		[TestCase("http://localhost/etag")]
		[TestCase("http://localhost/etag2")]
		public async Task GetContentShouldCorrectlyHandleETagHeaders(string uri)
		{
			// arrange
			var etagUri = new Uri(uri);
			var etag = "\"686897696a7c876b7e\"";
			var etagHeader = EntityTagHeaderValue.Parse(etag);
			var handlerMock = new Mock<HttpMessageHandler>();
			handlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.Is<HttpRequestMessage>(req =>
						req.RequestUri == etagUri &&
						req.Headers.IfNoneMatch.Contains(etagHeader)),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(() => 
					new HttpResponseMessage(HttpStatusCode.NotModified));
			handlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.Is<HttpRequestMessage>(req =>
						req.RequestUri == etagUri && !req.Headers.IfNoneMatch.Contains(etagHeader)),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(() => new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent($"{etag}_content"),
					Headers = { ETag = etagHeader }
				});
			handlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.Is<HttpRequestMessage>(req =>
						req.RequestUri != etagUri),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(() => new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent(Guid.NewGuid().ToString("N"))
				});
			var httpClientApp = new HttpClientApp(handlerMock.Object);

			// act
			var actualResponses = new[]
			{
				await httpClientApp.GetContentAsync(etagUri),
				await httpClientApp.GetContentAsync(etagUri)
			};

			// assert
			handlerMock.Protected().Verify(
				"SendAsync",
				Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req =>
					req.Method == HttpMethod.Get &&
					req.RequestUri == etagUri && !req.Headers.IfNoneMatch.Any()),
				ItExpr.IsAny<CancellationToken>());
			CollectionAssert.AreEquivalent(new []
			{
				$"{etag}_content",
				$"{etag}_content"
			}, actualResponses);
		}
	}
}