using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorMusic.Models;

namespace RazorMusic.Data
{
    public class RazorMusicContext : DbContext
    {
        public RazorMusicContext (DbContextOptions<RazorMusicContext> options)
            : base(options)
        {
        }

        public DbSet<RazorMusic.Models.Album> Album { get; set; } = default!;
    }
}
