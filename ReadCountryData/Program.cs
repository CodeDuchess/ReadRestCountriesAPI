using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace ReadCountryData
{
    class Program
    {
        public class Currency
        {

            public string Code { get; set; }

            public string Name { get; set; }

            public string Symbol { get; set; }
        }

        public class Country 
        {

            public List<Currency> Currencies { get; set; }

            public string Name { get; set; }

            public string Capital { get; set; }
        }

        static void Main(string[] args)
        {
            HttpClient http = new HttpClient();

            string baseUrl = "https://restcountries.eu/rest/v2/name/";

            string queryFilter = "?fields=name;capital;currencies";

            Console.WriteLine("Enter your country name:");

            string searchTerm = Console.ReadLine();

            string url = baseUrl + searchTerm + queryFilter;

            HttpResponseMessage response = http.GetAsync(new Uri(url)).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            var countries = JsonConvert.DeserializeObject<List<Country>>(responseBody);
            PrintCountryInfo(countries);
        }

        public static void PrintCountryInfo(List<Country> countries)
        {
            int counter = 0;
            foreach (var country in countries)
            {
                counter++;
                Console.WriteLine("#" + counter);
                Console.WriteLine("Country Name:" + country.Name);

                Console.WriteLine("Country Capital:" + country.Capital);

                foreach (var currency in country.Currencies)
                {
                    Console.WriteLine("Country Currency:" + currency.Name);

                    Console.WriteLine("Country Code:" + currency.Code);

                    Console.WriteLine("Country Symbol:" + currency.Symbol);
                }

                Console.WriteLine(".......................................................");
            }
            Console.ReadKey();

        }
    }
}
