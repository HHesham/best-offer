using System;
using System.Threading.Tasks;
using BestOffer.Domains;
using BestOffer.Models.Requests;
using BestOffer.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestOffer.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]/")]
    public class CompanyBController : ControllerBase
    {
        private readonly ILogger<CompanyBController> _logger;
        private readonly OffersDomain _offersDomain;

        public CompanyBController(ILogger<CompanyBController> logger, OffersDomain offersDomain)
        {
            _logger = logger;
            _offersDomain = offersDomain;
        }

        [HttpPost]
        [Route("/api/v1/[controller]/offers")]
        [Produces("application/json", Type = typeof(CompanyBOffersResponse))]
        public async Task<IActionResult> GetOffersAsync([FromBody] CompanyBOffersRequest request)
        {
            _logger.LogInformation($"Retrieving offer for controllerB for the given request {request}");
            CompanyBOffersResponse response = await _offersDomain.GetCompanyBOffer(request);
            return Ok(response);
        }
    }
}
