using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record ResourceDTO(int Id, string Title, string Description, string Url, bool Deprecated);

    public record ResourceCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; init; }

        [Required]
        public int UserId { get; init; }

        [Required]
        [StringLength(500)]
        public string Description { get; init; }

        [Required]
        public DateTime TimeOfReference { get; init; }

        public DateTime TimeOfResourcePublication { get; init; }

        [Required]
        public string Url { get; init; }

        public ISet<string> Tags { get; init; }

        public int InitialRating { get; init; }

        [Required]
        public Boolean Deprecated { get; init; }

        [Required]
        public DateTime LastCheckedForDeprecation { get; init; }
    }
}