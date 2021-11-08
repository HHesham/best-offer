using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BestOffer.src.Models;
using BestOffer.src.Models.Requests;
using BestOffer.src.Models.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BestOffer.src.Client
{
    public class OffersClient
    {
        private ILogger<OffersClient> _logger;
        private OffersHttpClient _client;
        private readonly string CompanyAKey;
        private readonly string CompanyBKey;
        private readonly string CompanyCKey;

        public OffersClient(ILogger<OffersClient> logger, IConfiguration config, OffersHttpClient client)
        {
            _client = client;
            _logger = logger;
            CompanyAKey = config["BestOffers:CompanyA"];
            CompanyBKey = config["BestOffers:CompanyB"];
            CompanyCKey = config["BestOffers:CompanyC"];
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<OffersRequest> GetBestOfferAsync(Dictionary<string, string> uris, Address source, Address destination, CartonDimentions[] cartonDimentions)
        {
            _logger.LogInformation($"Retrieving best offer for {uris}");
            Dictionary<OffersRequest, double> offersPrices = new Dictionary<OffersRequest, double>();

            if (uris.ContainsKey(CompanyAKey))
            {
                CompanyAOffersRequest requestA = new CompanyAOffersRequest
                {
                    ContactAddress = source,
                    WarehouseAddress = destination,
                    PackageDimensions = cartonDimentions
                };
                string requestBodyA = JsonSerializer.Serialize(requestA);
                var responseA = await _client.OnPostAsync(uris[CompanyAKey], requestBodyA);
                CompanyAOffersResponse companyAOffersResponse = JsonSerializer.Deserialize<CompanyAOffersResponse>(responseA);
                offersPrices.Add(companyAOffersResponse, companyAOffersResponse.Total);
            }
            if (uris.ContainsKey(CompanyBKey))
            {
                CompanyBOffersRequest requestB = new CompanyBOffersRequest
                {
                    Consignee = source,
                    Consignor = destination,
                    cartons = cartonDimentions
                };
                string requestBodyB = JsonSerializer.Serialize(requestB);
                var responseB = await _client.OnPostAsync(uris[CompanyBKey], requestBodyB);
                CompanyBOffersResponse companyBOffersResponse = JsonSerializer.Deserialize<CompanyBOffersResponse>(responseB);
                offersPrices.Add(companyBOffersResponse, companyBOffersResponse.Amount);
            }
            if (uris.ContainsKey(CompanyCKey))
            {
                CompanyCOffersRequest requestC = new CompanyCOffersRequest
                {
                    Source = source,
                    Destination = destination,
                    Packages = cartonDimentions
                };
                string requestBodyC = JsonSerializer.Serialize(requestC);
                var responseC = await _client.OnPostAsync(uris[CompanyCKey], requestBodyC);
                CompanyCOffersResponse companyCOffersResponse = JsonSerializer.Deserialize<CompanyCOffersResponse>(responseC);
                offersPrices.Add(companyCOffersResponse, companyCOffersResponse.Quote);
            }

            return offersPrices.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;            
        }
    }
}
