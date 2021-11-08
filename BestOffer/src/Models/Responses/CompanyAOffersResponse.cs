using System;
namespace BestOffer.Models.Responses
{
    public class CompanyAOffersResponse : OffersResponse
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
