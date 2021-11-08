using System;
namespace BestOffer.src.Models
{
    public class CartonDimentions
    {
        public CartonDimentions()
        {
        }

        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override string ToString()
        {
            return $"{nameof(Length)}: {Length}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height},";
        }
    }
}
