using System;
namespace BestOffer.Models.Responses
{
    public class CompanyBOffersResponse : OffersResponse
    {
        public CompanyBOffersResponse()
        {
        }

        public double Amount { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Amount)}: {Amount}";
        }
    }
}
