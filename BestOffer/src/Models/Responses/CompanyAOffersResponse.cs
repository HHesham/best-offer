using System;
namespace BestOffer.src.Models.Responses
{
    public class CompanyAOffersResponse : OffersRequest
    {
        public CompanyAOffersResponse()
        {
        }

        public double Total { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Total)}: {Total}";
        }
    }
}
