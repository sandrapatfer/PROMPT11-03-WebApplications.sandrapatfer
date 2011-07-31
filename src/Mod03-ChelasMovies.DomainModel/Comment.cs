using System;
using System.ComponentModel.DataAnnotations;

namespace Mod03_ChelasMovies.DomainModel
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        public String Description { get; set; }

        public enum CommentRating
        {
            ReallyBad,
            Bad,
            Average,
            Good,
            ReallyGood
        }

        public CommentRating Rating { get; set; }

        public int MovieID { get; set; }
        
        public virtual Movie Movie { get; set; }

        public int xpto { get; set; }


    }
}