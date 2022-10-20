using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorMusic.Data;
using RazorMusic.Models;

namespace RazorMusic.Pages.Albums
{
    public class EditModel : PageModel
    {
        private readonly RazorMusic.Data.RazorMusicContext _context;

        public EditModel(RazorMusic.Data.RazorMusicContext context)
        {
            _context = context;
        }

        [BindProperty] // *3.1
        public Album Album { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) // *2
        {
            if (id == null || _context.Album == null)
            {
                return NotFound();
            }

            var album =  await _context.Album.FirstOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }
            Album = album; // *3
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // *4
            {
                return Page();
            }

            _context.Attach(Album).State = EntityState.Modified;

            try // *1
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(Album.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AlbumExists(int id)
        {
          return _context.Album.Any(e => e.ID == id);
        }
    }
}

/*
 * 1. This try/catch block detects concurrency exceptions.
 * 
 * 2. The id parameter comes from the URL.
 * 
 * 3. After getting the album from the DB, setting this variable makes it avaliable to the page.
 * 
 * 3.1. But it is the Bind Property attribute which allows the connection between the page and the view model.
 * 
 * 4. This condition detects any exceptions to the input validations.
 * 
 */