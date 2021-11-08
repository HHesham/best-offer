using System;
namespace BestOffer.src.Models.Responses
{
    public class OffersRequest
    {
        public OffersRequest()
        {
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

    }
}
