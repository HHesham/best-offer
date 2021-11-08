using System;
using System.Xml.Serialization;

namespace BestOffer.src.Models.Responses
{
    [XmlRoot(ElementName = "Response")]
    public class CompanyCOffersResponse : OffersRequest
    {
        public CompanyCOffersResponse()
        {
        }

        [XmlElement(ElementName = "Quote")]
        public double Quote { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Quote)}: {Quote}";
        }
    }
}
