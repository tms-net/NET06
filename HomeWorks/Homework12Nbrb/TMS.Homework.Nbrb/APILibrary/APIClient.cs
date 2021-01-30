using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APILibrary.Models;
using Newtonsoft.Json; //добавить nuget packet - Newtonsoft.Json

namespace APILibrary
{
    public class APIClient
    {
        private Dictionary<int, int> dictionaryCurrencies;

        public APIClient()
        {
            dictionaryCurrencies = new Dictionary<int, int>();
        }

        // <summary>
        // Getting full json file with all currencies
        // </summary>
        // <returns>list Currencies</returns>
        private async Task<List<Currencies>> GetAllCurrenciesAsync()
        {
            HttpClient httpClient = new HttpClient();
            string request = "https://www.nbrb.by/api/exrates/currencies";
            HttpResponseMessage response = (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return ....(responseBody);
        }

        /// <summary>
        /// Receiving currency as a short list
        /// </summary>
        /// <param name="countCurrencies">Count elements need result</param>
        /// <returns>list short currencies</returns>
        public List<ShortCurrencies> GetShortCurrencies(int countCurrencies)
        {
            ...

            CreateDictionaryCurrencies(listCurrencieses);
                ...
           
            return ...;
        }

        /// <summary>
        /// Create dictionary for currencies
        /// </summary>
        /// <param name="listCurrencies">list currencies</param>
        private void CreateDictionaryCurrencies(List<Currencies> listCurrencies)
        {
            dictionaryCurrencies.Clear();

            foreach (var currency in listCurrencies)
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
        private async Task<Rates> GetRateOnDate(DateTime forDate, int codeCurrency)
        {
            HttpClient httpClient1 = new HttpClient();

            var searchDate = forDate.ToString("yyyy-M-d");
            var searchCode = dictionaryCurrencies.FirstOrDefault(x => x.Key == codeCurrency).Value;

            string request = "https://www.nbrb.by/api/exrates/rates/" + searchCode + "?ondate=" + searchDate;
            HttpResponseMessage response = (await httpClient1.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Rates>(responseBody);
        }

        /// <summary>
        /// Get rates on date
        /// </summary>
        /// <param name="forDate">Date currency</param>
        /// <param name="codeCurrency">Code currency</param>
        /// <returns>Currency</returns>
        public Rates GetRates(DateTime forDate, int codeCurrency)
        {
            return GetRateOnDate(forDate, codeCurrency).Result;
        }

        // <summary>
        // Get list rates
        // </summary>
        // <param name = "startDate" > Start date</param>
        // <param name = "finishDate" > Finish date</param>
        // <param name = "codeCurrency" > Code currency</param>
        // <returns>List Currencies</returns>
        public List<Rates> GetRates(DateTime startDate, DateTime finishDate, int codeCurrency)
        {
            return GetRatesOnPeriod(startDate, finishDate, codeCurrency).Result;
        }





    }
}
