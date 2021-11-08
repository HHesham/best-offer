using System;
using System.Xml.Serialization;

namespace BestOffer.Models.Responses
{
    [XmlRoot(ElementName = "Response")]
    public class CompanyCOffersResponse : OffersResponse
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
