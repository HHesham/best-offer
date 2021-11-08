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
    public class CompanyAController : ControllerBase
    {

        private readonly ILogger<CompanyAController> _logger;
        private readonly OffersDomain _offersDomain;

        public CompanyAController(ILogger<CompanyAController> logger, OffersDomain offersDomain)
        {
            _logger = logger;
            _offersDomain = offersDomain;
        }

        [HttpPost]
        [Route("/api/v1/[controller]/offers")]
        [Produces("application/json", Type = typeof(CompanyAOffersResponse))]
        public async Task<IActionResult> GetOffersAsync([FromBody] CompanyAOffersRequest request)
        {
            _logger.LogInformation($"Retrieving offer for controllerA for the given request {request}");
            CompanyAOffersResponse response = await _offersDomain.GetCompanyAOffer(request);
            return Ok(response);
        }
    }
}
