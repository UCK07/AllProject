using AllProject.Shared.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace AllProject.Client.Services
{
    public class CommonService
    {
        private readonly RestClient _restClient;
        
        private static string _apiKey = "apikey 5xYHum0bvX2CbJk1AQT7iS:7p4PqbKmv4NE4d5YkZTR0B";
        private static string _apiUrl = "https://api.collectapi.com/";
        public CommonService(RestClient restClient)
        {
            _restClient = restClient;
        }

       
        public async Task<PharmacyDto.Root> GetPharmacyList(string cityname)
        {
            PharmacyDto.Root news = new();

            var client = new RestClient(_apiUrl);

            var request = new RestRequest("health/dutyPharmacy?/", Method.Get);
            request.AddParameter("il", cityname );
            request.AddHeader("authorization", _apiKey);

            var response = await client.ExecuteAsync<PharmacyDto.Root>(request);
            news = JsonConvert.DeserializeObject<PharmacyDto.Root>(response.Content);
            return news;
        }
        public async Task<DictionaryDto.Root> GetDictionaryList(string WordName)
        {
            DictionaryDto.Root newss = new();

            var client = new RestClient(_apiUrl);

            var request = new RestRequest("dictionary/wordSearchTurkish?", Method.Get);
            request.AddParameter("query", WordName);
            request.AddHeader("authorization", _apiKey);

            var response = await client.ExecuteAsync<DictionaryDto.Root>(request);
            newss = JsonConvert.DeserializeObject<DictionaryDto.Root>(response.Content);
            return newss;
        }


    }
}
