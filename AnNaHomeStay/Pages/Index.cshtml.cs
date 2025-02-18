using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AnNaHomeStay.Pages
{
    public class SearchDto
    {
        public string SearchString { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
    }

    public class HomeStayDto
    {
        public Homestay Homestay { get; set; }
        public decimal PriceWhenSell { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AnNaHomeStayContext _context;

        public IndexModel(ILogger<IndexModel> logger, AnNaHomeStayContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Homestay> Homestays { get; set; }
        public List<HomeStayDto> HomeStayDtos { get; set; }
        [BindProperty]
        public SearchDto Search { get; set; } = default!;

        public void OnGet()
        {
            Homestays = _context.Homestays
                .Include(h => h.Discounts)
                .Include(h => h.Images)
                .Where(h => h.Status)
                .ToList();
            Search = new SearchDto();
            HomeStayDtos = Homestays.Select(h => new HomeStayDto
            {
                Homestay = h,
                PriceWhenSell = getPriceSell(h)
            }).ToList();
        }

        public void OnPost()
        {
            var query = _context.Homestays.Include(h => h.Images).Where(h => h.Status);

            if (!string.IsNullOrEmpty(Search?.SearchString))
            {
                query = query.Where(h => h.HomestayName.Contains(Search.SearchString));
            }


            Homestays = query.ToList();

            HomeStayDtos = Homestays.Select(h => new HomeStayDto
            {
                Homestay = h,
                PriceWhenSell = getPriceSell(h)
            }).ToList();

        }

        private decimal getPriceSell(Homestay homestay)
        {
            var discount = homestay.Discounts;
            var priceWhenSell = homestay.Price;
            var currentDate = DateTime.Now;
            if (discount != null)
            {
                decimal totalDiscount = discount
                    .Where(d => d.DateStart <= currentDate && d.DateEnd >= currentDate)
                    .Sum(x => ((decimal)x.Discount1 / 100) * priceWhenSell);
                priceWhenSell -= totalDiscount;
            }
            return priceWhenSell;
        }
    }
}