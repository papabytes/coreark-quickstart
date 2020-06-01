using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Portal.Clients
{
    public class ApiHttpMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public ApiHttpMessageHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>(Constants.ACCESS_TOKEN_KEY);
            if(!request.Headers.Contains("Authorization"))
                request.Headers.Add("Authorization", "Bearer " + token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}