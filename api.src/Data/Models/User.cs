using System.Collections.Generic;

namespace Data
{
    public class User
    {
        public string Id { get; set; }

        public ICollection<Resource> Resources { get ; set; } = new List<Resource>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    }

}