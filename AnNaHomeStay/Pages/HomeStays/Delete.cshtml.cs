using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnNaHomeStay.Pages.HomeStays
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AnNaHomeStay.Models.AnNaHomeStayContext _context;

        public DeleteModel(AnNaHomeStay.Models.AnNaHomeStayContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Homestay Homestay { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Homestays == null)
            {
                return NotFound();
            }

            var homestay = await _context.Homestays.FirstOrDefaultAsync(m => m.HomestayId == id && m.Status);

            if (homestay == null)
            {
                return NotFound();
            }
            else
            {
                Homestay = homestay;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null || _context.Homestays == null)
            {
                return NotFound();
            }
            var homestay = _context.Homestays
                .SingleOrDefault(h => h.HomestayId == id && h.Status);

            if (homestay != null)
            {
                Homestay = homestay;
                Homestay.Status = false;
                _context.Homestays.Update(Homestay);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
