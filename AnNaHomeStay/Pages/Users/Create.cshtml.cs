using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnNaHomeStay.Models;
using AnNaHomeStay.Contants;
using Microsoft.AspNetCore.Authorization;

namespace AnNaHomeStay.Pages.Users
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
            ViewData["Roles"] = new SelectList(Roles.instance);
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            if (IsEmailExist(User.Email))
            {
                ModelState.AddModelError("Error", "Email is already exist.");
                return Page();
            }

            if (IsUserNameExist(User.Username))
            {
                ModelState.AddModelError("Error", "Username is already exist.");
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private Boolean IsUserNameExist(string userName)
        {
            return _context.Users.Any(u => u.Username == userName);
        }

        private Boolean IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
