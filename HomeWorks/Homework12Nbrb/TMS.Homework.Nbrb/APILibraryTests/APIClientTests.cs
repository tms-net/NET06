using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using APILibrary;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace APILibraryTests
{
	public class ApiClientTests
	{
		private const string USDJson =
			@"{""Cur_ID"":145,""Cur_ParentID"":145,""Cur_Code"":""840"",""Cur_Abbreviation"":""USD"",""Cur_Name"":""Доллар США"",""Cur_Name_Bel"":""Долар ЗША"",""Cur_Name_Eng"":""US Dollar"",""Cur_QuotName"":""1 Доллар США"",""Cur_QuotName_Bel"":""1 Долар ЗША"",""Cur_QuotName_Eng"":""1 US Dollar"",""Cur_NameMulti"":""Долларов США"",""Cur_Name_BelMulti"":""Долараў ЗША"",""Cur_Name_EngMulti"":""US Dollars"",""Cur_Scale"":1,""Cur_Periodicity"":0,""Cur_DateStart"":""1991-01-01T00:00:00"",""Cur_DateEnd"":""2050-01-01T00:00:00""}";

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task GetShortCurrenciesAsyncShouldFetchCurrenciesFromNbrbApi()
		{
			// arrange
			var nbrbApiResponse = new HttpResponseMessage {
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent($"[{USDJson}]")
			};
			var handlerMock = new Mock<HttpMessageHandler>();
			handlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(nbrbApiResponse);
			var apiClient = new APIClient(handlerMock.Object);

			// act
			var currencies = await apiClient.GetShortCurrenciesAsync();

			//assert
			handlerMock.Protected().Verify(
				"SendAsync",
				Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req => 
					req.Method == HttpMethod.Get && req.RequestUri.AbsoluteUri == "https://www.nbrb.by/api/exrates/currencies"),
				ItExpr.IsAny<CancellationToken>());
			CollectionAssert.IsNotEmpty(currencies);
			Assert.AreEqual(1, currencies.Count);
		}

		[Test]
		public void GetShortCurrenciesAsyncShouldHandleErrorsFromNbrbApi()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void GetShortCurrenciesAsyncShouldNotReturnExcludedCurrencies()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void GetShortCurrenciesAsyncShouldReturnCurrenciesInOrder()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void GetRatesAsyncShouldGetCurrencyIDsFromNbrbApiExactlyOnce()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}

		[Test]
		public void GetRatesAsyncShouldHandleErrorsFromNbrbApi()
		{
			// arrange

			// act

			//assert
			Assert.Fail();
		}
	}
}