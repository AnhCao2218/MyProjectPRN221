﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly AnNaHomeStay.Models.AnNaHomeStayContext _context;

        public IndexModel(AnNaHomeStay.Models.AnNaHomeStayContext context)
        {
            _context = context;
        }

        public IList<Discount> Discount { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Discounts != null)
            {
                Discount = await _context.Discounts
                    .Include(d => d.Homstay)
                    .Include(d => d.Homstay).ToListAsync();
            }
        }
    }
}
