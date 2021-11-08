using System;
using System.Threading.Tasks;
using BestOffer.src.Models.Responses;
using Microsoft.Extensions.Logging;
using BestOffer.src.Models.Requests;

namespace BestOffer.src.Domains
{
    public class OffersDomain
    {
        private readonly ILogger<OffersDomain> _logger;

        public OffersDomain(ILogger<OffersDomain> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This function will exeucte the logic for CompanyA
        /// and fetch the offer for the giveng request.
        /// </summary>
        public async Task<CompanyAOffersResponse> GetCompanyAOffer(CompanyAOffersRequest request)
        {
            _logger.LogInformation($"Getting offer for CompanyA for request {request}");
            CompanyAOffersResponse response = await Task.Run(() =>
            {
                return new CompanyAOffersResponse
                {
                    Total = 10.0
                };
            });
            return response;
        }


        /// <summary>
        /// This function will exeucte the logic for CompanyB
        /// and fetch the offer for the giveng request.
        /// </summary>
        public async Task<CompanyBOffersResponse> GetCompanyBOffer(CompanyBOffersRequest request)
        {
            _logger.LogInformation($"Getting offer for CompanyB for request {request}");
            CompanyBOffersResponse response = await Task.Run(() =>
            {
                return new CompanyBOffersResponse
                {
                    Amount = 30.0
                };
            });
            return response;
        }


        /// <summary>
        /// This function will exeucte the logic for CompanyC
        /// and fetch the offer for the giveng request.
        /// </summary>
        public async Task<CompanyCOffersResponse> GetCompanyCOffer(CompanyCOffersRequest request)
        {
            _logger.LogInformation($"Getting offer for CompanyC for request {request}");
            CompanyCOffersResponse response = await Task.Run(() =>
            {
                return new CompanyCOffersResponse
                {
                    Quote = 20.0
                };
            });
            return response;
        }

    }
}
