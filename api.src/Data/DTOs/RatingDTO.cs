using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
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
        [Required]
        public int Id { get; init; }
        
        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int UpdatedRating { get; init; }
    }
}