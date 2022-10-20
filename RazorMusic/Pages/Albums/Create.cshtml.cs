using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorMusic.Data;
using RazorMusic.Models;

namespace RazorMusic.Pages.Albums
{
    public class CreateModel : PageModel
    {
        private readonly RazorMusic.Data.RazorMusicContext _context;

        public CreateModel(RazorMusic.Data.RazorMusicContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] // *3
        public Album Album { get; set; } // *1
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid) // *4
            {
                return Page();
            }

            _context.Album.Add(Album); // *2
            await _context.SaveChangesAsync(); // *2

            return RedirectToPage("./Index");
        }
    }
}

/*
 * 1. This stores the input from the user on the page.
 * 
 * 2. Here the input data is addded to and saved in the context.
 * 
 * 3. This attribute makes the connection between the page and the property.
 * 
 * 4. This condition detects any exceptions to the input validations.
 * 
 */