using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    /*
    public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Resource Resource { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rated { get; set; }
    */
   // public record RatingDTO(int Id, string Title, string Description, string Url, bool Deprecated);
    public record RatingDTO(int Id, string Title, string Description , bool Deprecated);

    public record RatingDetailsDTO(int Id, string UserId, int ResourceId, int Rated);
    public record RatingCreateDTO
    {
        [Required]
        public string UserId { get; init; }

        [Required]
        public int ResourceId { get; init; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rated { get; init; }
    }

    public record RatingUpdateDTO 
    {
        public int Id { get; init; }
        
        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int UpdatedRating { get; init; }
    }
}