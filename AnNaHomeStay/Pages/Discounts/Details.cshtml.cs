using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnNaHomeStay.Pages.Discounts
{
    [Authorize(Policy = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly AnNaHomeStay.Models.AnNaHomeStayContext _context;

        public DetailsModel(AnNaHomeStay.Models.AnNaHomeStayContext context)
        {
            _context = context;
        }

        public Discount Discount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }
            else
            {
                Discount = discount;
            }
            return Page();
        }
    }
}
