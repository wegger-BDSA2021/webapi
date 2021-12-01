using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record ResourceDTO(int Id, string Title, string Description, string Url, double AverageRating, bool Deprecated);

    public record ResourceDetailsDTO(int Id, string Title, string Description, DateTime TimeOfReference, DateTime TimeOfResourcePublication, string Url, IReadOnlyCollection<string> Tags, IReadOnlyCollection<int> Ratings, double AverageRating, IReadOnlyCollection<string> Comments, bool Deprecated, DateTime LastCheckedForDeprecation) : ResourceDTO(Id, Title, Description, Url, AverageRating, Deprecated);

    // the one being built by the resourcebuilder, and consumed by the resource_repo
    public record ResourceCreateDTOServer
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

        [Required]
        public DateTime TimeOfResourcePublication { get; init; }

        [Required]
        public string Url { get; init; }

        public ISet<string> Tags { get; init; } = new HashSet<string>();

        [Required]
        public int InitialRating { get; init; }

        [Required]
        public Boolean Deprecated { get; init; }

        [Required]
        public DateTime LastCheckedForDeprecation { get; init; }
    }

    // the dto consumed by the api controller
    public record ResourceCreateDTOClient
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
        public string Url { get; init; }

        [Required]
        public int InitialRating { get; init; }
    }

    public record ResourceUpdateDTO 
    {
        [Required]
        public int Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public int? UserId { get; init; }
        public DateTime? TimeOfResourcePublication { get; init; }
        public string? Url { get; init; }
        public ISet<string>? Tags { get; init; }
        public Boolean? Deprecated { get; init; }
        public DateTime? LastCheckedForDeprecation { get; init; }
    }
}