using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorMusic.Data;
using RazorMusic.Models;

namespace RazorMusic.Pages.Albums
{
    public class DeleteModel : PageModel
    {
        private readonly RazorMusic.Data.RazorMusicContext _context;

        public DeleteModel(RazorMusic.Data.RazorMusicContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Album == null)
            {
                return NotFound();
            }

            var album = await _context.Album.FirstOrDefaultAsync(m => m.ID == id);

            if (album == null)
            {
                return NotFound();
            }
            else 
            {
                Album = album;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Album == null)
            {
                return NotFound();
            }
            var album = await _context.Album.FindAsync(id);

            if (album != null)
            {
                Album = album;
                _context.Album.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
