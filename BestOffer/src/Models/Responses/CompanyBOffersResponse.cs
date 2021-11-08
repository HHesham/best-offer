using System;
namespace BestOffer.src.Models.Responses
{
    public class CompanyBOffersResponse : OffersRequest
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
