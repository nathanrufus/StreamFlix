using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamFlix.Models
{
    public class Movie
    {
        [Key]
        public string MovieId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Genre { get; set; }
        
        public string Director { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public double Rating { get; set; }
        
        // URL for the main video file stored in S3
        public string S3Url { get; set; }
        
        // URL for the movie image stored in S3
        public string ImageUrl { get; set; }
        
        public List<Comment> Comments { get; set; }
    }
}
