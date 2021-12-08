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
    public record TagDTO(int Id, string Name, bool Deprecated);
    public record TagDetailsDTO(int Id, string Name,  IReadOnlyCollection<string> ResourcesNames);


    public record TagCreateDTO
    {
        [Required]
        public string Name { get; init; }
    }

    public record TagUpdateDTO : TagCreateDTO
    {
        public int Id { get; init; }
        [Required]
        public int NewName { get; init; }
    }
}