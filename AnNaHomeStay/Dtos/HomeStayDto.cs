using AnNaHomeStay.Models;

namespace AnNaHomeStay.Dtos
{
    public class HomeStayDto
    {
        public Homestay Homestay { get; set; }
        public decimal PriceWhenSell { get; set; }

        public bool isHasDiscount()
        {
            return PriceWhenSell != Homestay.Price;
        }
    }
}
