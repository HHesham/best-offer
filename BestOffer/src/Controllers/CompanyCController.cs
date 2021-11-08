using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BestOffer.Domains;
using BestOffer.Models.Requests;
using BestOffer.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestOffer.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]/")]
    public class CompanyCController : ControllerBase
    {
        private readonly ILogger<CompanyCController> _logger;
        private readonly OffersDomain _offersDomain;

        public CompanyCController(ILogger<CompanyCController> logger, OffersDomain offersDomain)
        {
            _logger = logger;
            _offersDomain = offersDomain;
        }

        [HttpPost]
        [Route("/api/v1/[controller]/offers")]
        public async Task<IActionResult> PostAsync(string request)
        {
            string xml;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                xml = await reader.ReadToEndAsync();
            }
            _logger.LogInformation($"{xml}");
            XmlSerializer serializer = new XmlSerializer(typeof(CompanyCOffersRequest));
            StringReader strReader = new StringReader(xml);
            CompanyCOffersRequest offersRequest = (CompanyCOffersRequest)serializer.Deserialize(strReader);

            _logger.LogInformation($"Retrieving offer for controllerC for the given request {offersRequest}");
            CompanyCOffersResponse response = await _offersDomain.GetCompanyCOffer(offersRequest);

            OkObjectResult result = Ok(response);
            result.Formatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
            return result;
        }

    }
}
