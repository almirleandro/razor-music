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
    public class IndexModel : PageModel
    {
        private readonly RazorMusic.Data.RazorMusicContext _context; // *1

        public IndexModel(RazorMusic.Data.RazorMusicContext context) // *1
        {
            _context = context;
        }

        public IList<Album> Album { get;set; } = default!;

        [BindProperty(SupportsGet = true)] // *4
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? AlbumGenre { get; set; }

        public async Task OnGetAsync() // *2
        {
            //if (_context.Album != null)
            //{
            //    Album = await _context.Album.ToListAsync(); // *3
            //}

            IQueryable<string> genreQuery = from m in _context.Album // *7
                                            orderby m.Genre
                                            select m.Genre;

            var albums = from m in _context.Album // *5
                         select m;

            if (!string.IsNullOrEmpty(SearchString)) // *6
            {
                albums = albums.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(AlbumGenre)) // *6
            {
                albums = albums.Where(x => x.Genre == AlbumGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Album = await albums.ToListAsync();
        }
    }
}

/*
 * 1. These lines use Dependency Injection. It's safer and more convinient than just implementing a class.
 * They bring context to this page.
 * 
 * 2. OnGetAsync() runs on page request.
 * 
 * 3. This variable stores the data from the DB (a list of all the albums). It's now avaliable to be used on the page.
 * 
 * 4. [BindProperty] binds form values and query strings with the same name as the property.
 * The SupportsGet = true allows for passing parameters on a GET request (for security reasons, since it's more common
 * on POST requests).
 * 
 * 5. This creates a LINQ query to select the movies (just the query. It's not run yet).
 * 
 * 6. If there is a search term or filter, the LINQ query is modified.
 * 
 * 7. This creates a LINQ query that retrieves all the genres from the database.
 * 
 */