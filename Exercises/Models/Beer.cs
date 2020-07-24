using System.Collections.Generic;

namespace Exercises.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BeerUserRatings> UserRatings { get; set; }
    }
}
