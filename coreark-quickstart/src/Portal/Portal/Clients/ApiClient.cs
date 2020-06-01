using System.Net.Http;
using Blazored.LocalStorage;

namespace Portal.Clients
{
    public class ApiClient : HttpClient
    {
        private readonly ILocalStorageService _localStorageService;

        public HttpClient Client { get; }

        public ApiClient(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            Client = httpClient;
        }
    }
}