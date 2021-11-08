using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BestOffer.src.Client
{

    public class OffersHttpClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<OffersHttpClient> _logger;

        public OffersHttpClient(HttpClient client, IConfiguration config, ILogger<OffersHttpClient> logger)
        {
            _logger = logger;
            _client = client;
            _client.BaseAddress = new Uri(config["BestOffers:ServiceUrl"]);
        }

        public async Task<string> OnGetAsync(string uri)
        {
            var response = await _client.GetAsync(uri);
            await ThrowIfUnsuccessfulAsync(response);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> OnPostAsync(string uri, string data)
        {
            var content = new StringContent(
                data,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync(uri, content);
            await ThrowIfUnsuccessfulAsync(response);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task ThrowIfUnsuccessfulAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.Content != null)
            {
                try
                {
                    string errorResponse = await response.Content!.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception caught when trying to get the Content of an error response (status={response.StatusCode}).  {ex}");
                }
            }
        }
    }
}
