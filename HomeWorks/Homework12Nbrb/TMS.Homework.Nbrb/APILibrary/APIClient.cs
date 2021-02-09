using APILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace APILibrary
{
    public class APIClient
    {
        /// <summary>
        /// Dictionary for currencies (save code currency and internal code)
        /// </summary>
        private Dictionary<int, int> dictionaryCurrencies;

        private static HttpClient httpClient = new HttpClient();

        public APIClient()
        {
            dictionaryCurrencies = new Dictionary<int, int>();
        }

        /// <summary>
        /// Getting full json file with all currencies
        /// </summary>
        /// <returns>list Currency</returns>
        private async Task<List<Currency>> GetAllCurrenciesAsync() => await httpClient.GetFromJsonAsync<List<Currency>>("https://www.nbrb.by/api/exrates/currencies");

        /// <summary>
        /// Receiving currency as a short list (Получение полного списка валют)
        /// </summary>
        /// <returns>list short currencies</returns>
        public async Task<List<ShortCurrency>> GetShortCurrenciesAsync()
        {
            var listCurrencies = (await GetAllCurrenciesAsync()).ToList();

            CreateDictionaryCurrencies(listCurrencies);

            return listCurrencies.Where(x => x.Cur_DateEnd > DateTime.Now)
                .OrderBy(y => y.Cur_Code)
                .Select(c => new ShortCurrency()
                {
                    Name = c.Cur_Name,
                    Abbreviation = c.Cur_Abbreviation,
                    Code = c.Cur_Code
                })
                .ToList();
        }

        /// <summary>
        /// Create dictionary for currencies
        /// </summary>
        /// <param name="listCurrencies">list currencies</param>
        private void CreateDictionaryCurrencies(List<Currency> listCurrencies)
        {
            dictionaryCurrencies.Clear();

            foreach (var currency in listCurrencies.Where(x => x.Cur_DateEnd > DateTime.Now))
            {
                dictionaryCurrencies.Add(currency.Cur_Code, currency.Cur_ID);
            }
        }

        /// <summary>
        /// Task for get Rates
        /// </summary>
        /// <param name="forDate">Date currency</param>
        /// <param name="codeCurrency">Code currency</param>
        /// <returns>Task</returns>
        private async Task<Rate> GetRateOnDateAsync(DateTime forDate, int codeCurrency)
        {
            CreateDictionaryCurrencies((await GetAllCurrenciesAsync()).ToList());

            var searchDate = forDate.ToString("yyyy-M-d");
            var searchCode = dictionaryCurrencies.FirstOrDefault(x => x.Key == codeCurrency).Value;

            var request = "https://www.nbrb.by/api/exrates/rates/" + searchCode + "?ondate=" + searchDate;
            return await httpClient.GetFromJsonAsync<Rate>(request);
        }

        /// <summary>
        /// Get rates on date (получение курса валюты на дату)
        /// </summary>
        /// <param name="forDate">Date currency</param>
        /// <param name="codeCurrency">Code currency</param>
        /// <returns>Rate</returns>
        public Task<Rate> GetRatesAsync(DateTime forDate, int codeCurrency) => GetRateOnDateAsync(forDate, codeCurrency);

        /// <summary>
        /// Get list short rates (получение курса валюты за указанный период)
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="finishDate">Finish date</param>
        /// <param name="codeCurrency">Code currency</param>
        /// <returns>List short rate</returns>
        public async Task<List<ShortRate>> GetRatesAsync(DateTime startDate, DateTime finishDate, int codeCurrency) => await GetRatesOnPeriodAsync(startDate, finishDate, codeCurrency);

        /// <summary>
        /// Task for get list rates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="finishDate">Finish date</param>
        /// <param name="codeCurrency">Code currency</param>
        /// <returns>Task</returns>
        private async Task<List<ShortRate>> GetRatesOnPeriodAsync(DateTime startDate, DateTime finishDate, int codeCurrency)
        {
            CreateDictionaryCurrencies((await GetAllCurrenciesAsync()).ToList());

            var searchFirstDate = startDate.ToString("yyyy-M-d");
            var searchFinishDate = finishDate.ToString("yyyy-M-d");
            var searchCode = dictionaryCurrencies.FirstOrDefault(x => x.Key == codeCurrency).Value;

            var request = "https://www.nbrb.by/API/ExRates/Rates/Dynamics/" + searchCode + "?startDate=" + searchFirstDate + "&endDate=" + searchFinishDate;
            return await httpClient.GetFromJsonAsync<List<ShortRate>>(request);
        }
    }
}
