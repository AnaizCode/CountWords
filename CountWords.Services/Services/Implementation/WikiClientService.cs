using CountWords.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;


namespace CountWords.Services.Services.Implementation
{
    public class WikiClientService : IWikiClientService

    {
        
        private readonly HttpClient _httpClient;
        public WikiClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FindTextServiceClient");
        }



        public string FindText(string word)
        {
            var message = CreateRequestMessage(word);
            var response = SendRequest(message);
            response.EnsureSuccessStatusCode();
            string text = GetResponseContent(response);
            return text;
        }

        private HttpRequestMessage CreateRequestMessage(string word)
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string newBaseUrl = _httpClient.BaseAddress.ToString() + word;
            //_httpClient.BaseAddress = new Uri(newBaseUrl);
            message.RequestUri = new Uri(newBaseUrl);
            return message;
        }

        private HttpResponseMessage SendRequest(HttpRequestMessage message)
        {
            return _httpClient.SendAsync(message).Result;
        }

        private string GetResponseContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
