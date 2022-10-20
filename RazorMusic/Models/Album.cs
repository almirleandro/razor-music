using System.ComponentModel.DataAnnotations;

namespace RazorMusic.Models
{
    public class Album
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;

        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        [Range(0, 5)]
        public int Ranking { get; set; }
    }
}

/*
 * Entity Framework Core creates a database based on this class
 * 
 * Data Annotations add validation or text formatting to the application.
 * 
 */