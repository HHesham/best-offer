using System;
namespace BestOffer.src.Models
{
    public class Address
    {
        public Address()
        {
        }

        public float lat { get; set; }
        public float lng { get; set; }

        public override string ToString()
        {
            return $"{nameof(lat)}: {lat}, {nameof(lng)}: {lng}";
        }
    }
}
