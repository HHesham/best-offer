using System;
using System.Xml.Serialization;

namespace BestOffer.src.Models.Requests
{
    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class CompanyCOffersRequest
    {
        public CompanyCOffersRequest()
        {
        }

        [XmlElement(ElementName = "Source")]
        public Address Source { get; set; }

        [XmlElement(ElementName = "Destination")]
        public Address Destination { get; set; }

        [XmlElement(ElementName = "Packages")]
        public CartonDimentions[] Packages { get; set; }


        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}, {nameof(Destination)}: {Destination} ";
        }
    }
}
