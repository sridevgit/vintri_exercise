using System;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models
{
    public class BeerUserRatings
    {
        [Required]
        public string UserName { get; set; }

        [Range(1, 5)]
        [Required]
        public decimal Rating { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
