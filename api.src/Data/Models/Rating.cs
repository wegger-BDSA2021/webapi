using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Rating
    {
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        public Resource Resource { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rated { get; set; }

    }
}