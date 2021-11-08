using System;

namespace BestOffer.src.Models.Requests
{
    public class CompanyBOffersRequest
    {
        public CompanyBOffersRequest()
        {
        }

        public Address Consignee { get; set; }
        public Address Consignor { get; set; }
        public CartonDimentions[] cartons { get; set; }


        public override string ToString()
        {
            return $"{nameof(Consignee)}: {Consignee}, {nameof(Consignor)}: {Consignor} ";
        }
    }
}
