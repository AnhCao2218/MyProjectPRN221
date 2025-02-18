using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnNaHomeStay.Pages.Discounts
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AnNaHomeStay.Models.AnNaHomeStayContext _context;

        public CreateModel(AnNaHomeStay.Models.AnNaHomeStayContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["HomestayName"] = new SelectList(_context.Homestays, "HomestayId", "HomestayName");
            return Page();
        }

        [BindProperty]
        public Discount Discount { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Discounts == null || Discount == null)
            {
                ViewData["HomestayName"] = new SelectList(_context.Homestays, "HomestayId", "HomestayName");
                return Page();
            }

            _context.Discounts.Add(Discount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
