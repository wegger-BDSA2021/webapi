using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Rating
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        #nullable enable
        public string? UserId { get; set; }
        #nullable disable

        [ForeignKey("ResourceId")]
        public Resource Resource { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rated { get; set; }

    }
}