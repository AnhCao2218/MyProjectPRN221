using AnNaHomeStay.Dtos;
using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AnNaHomeStay.Pages.Login
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly AnNaHomeStayContext _context;

        public IndexModel(AnNaHomeStayContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserRequest UserRequest { get; set; }

        public void OnGet()
        {
            var user = HttpContext.User;
            var username = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username != null)
            {
                Response.Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User? user = _context.Users.SingleOrDefault(
                    x => x.Username == UserRequest.Username && x.Password == UserRequest.Password && x.Status == true
                    );
                if (user == null)
                {
                    ModelState.AddModelError("Error", "Wrong username or password.");
                    return Page();
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim("Role", user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                            );

                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}
