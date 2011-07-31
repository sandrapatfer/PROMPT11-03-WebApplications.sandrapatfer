using System.Collections.Generic;

namespace Mod03_ChelasMovies.DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class Movie
    {
        public Movie()
        {
            Year = DateTime.Now.Year;
        }

        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]  
        public string Title { get; set; }

        //[Required]
        [Range(1900, 2100, ErrorMessage = "The Year must be between 1900 and 2100")]
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Image { get; set; }
        public TimeSpan Runtime { get; set; }
        //public DateTime Xpto { get; set; }

        //[ScaffoldColumn(false)]
        //public string Hidden { get; set; }

        
        //[StringLength(5)]


        public virtual ICollection<Comment> Comments { get; set; }
    }
}