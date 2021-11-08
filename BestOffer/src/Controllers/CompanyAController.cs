using System.Threading.Tasks;
using BestOffer.src.Domains;
using BestOffer.src.Models.Requests;
using BestOffer.src.Models.Responses;
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




/*
 * 
 * 
- Input: Different signatures(name and params)
    int MyMethod(int i) { ... }
    int MyMethod(double d) { ... }
- Output: Differetn format

- APIs: different urls and credentials 

offer obj: id, value, expirateion date
 */