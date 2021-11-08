using System;

namespace BestOffer.src.Models.Requests
{
    public class CompanyAOffersRequest
    {
        public CompanyAOffersRequest()
        {
        }

        public Address ContactAddress { get; set; }
        public Address WarehouseAddress { get; set; }
        public CartonDimentions[] PackageDimensions { get; set; }


        public override string ToString()
        {
            return $"{nameof(ContactAddress)}: {ContactAddress}, {nameof(WarehouseAddress)}: {WarehouseAddress} ";
        }
    }
}
