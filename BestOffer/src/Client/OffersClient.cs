using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BestOffer.Models;
using BestOffer.Models.Requests;
using BestOffer.Models.Responses;
using BestOffer.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Xml.Serialization;
using System.IO;

namespace BestOffer.Client
{
    public class OffersClient
    {
        private ILogger<OffersClient> _logger;
        private OffersHttpClient _client;
        private readonly string CompanyAKey;
        private readonly string CompanyBKey;
        private readonly string CompanyCKey;

        public OffersClient(ILogger<OffersClient> logger, IOptions<ConfigKeys> config, OffersHttpClient client)
        {
            _client = client;
            _logger = logger;
            CompanyAKey = config.Value.CompanyAKey;
            CompanyBKey = config.Value.CompanyBKey;
            CompanyCKey = config.Value.CompanyCKey;
        }


        /// <summary>
        /// Get best offer for the given companies uris and the given dataset
        /// {source}{destination}{cartonDimentions}
        /// </summary>
        public async Task<OffersResponse> GetBestOfferAsync(Dictionary<string, string> uris, Address source, Address destination, CartonDimentions[] cartonDimentions)
        {
            _logger.LogInformation($"Retrieving best offer for {uris}");
            Dictionary<OffersResponse, double> offersPrices = new Dictionary<OffersResponse, double>();

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
                CompanyAOffersResponse companyAOffersResponse =
                    JsonSerializer.Deserialize<CompanyAOffersResponse>(responseA, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                CompanyBOffersResponse companyBOffersResponse =
                    JsonSerializer.Deserialize<CompanyBOffersResponse>(responseB, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                var serializer = new XmlSerializer(typeof(CompanyCOffersResponse));
                CompanyCOffersResponse companyCOffersResponse;
                using (TextReader reader = new StringReader(responseC))
                {
                    companyCOffersResponse = (CompanyCOffersResponse)serializer.Deserialize(reader);
                }
                offersPrices.Add(companyCOffersResponse, companyCOffersResponse.Quote);
            }

            foreach (KeyValuePair<OffersResponse, double> entry in offersPrices)
            {
                Console.WriteLine(entry.Key+ " "+entry.Value);   
            }
            return offersPrices.OrderBy(offer => offer.Value).First().Key;            
        }
    }
}
