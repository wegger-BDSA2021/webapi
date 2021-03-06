using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record ResourceDTO(int Id, string Title, string Description, string Url, double AverageRating, bool Deprecated);

    public record ResourceDetailsDTO(int Id, string Title, string SourceTitle, string Description, DateTime TimeOfReference, string Url, string HostBaseUrl, IReadOnlyCollection<string> Tags, IReadOnlyCollection<int> Ratings, double AverageRating, IReadOnlyCollection<string> Comments, bool Deprecated, DateTime LastCheckedForDeprecation, bool IsVideo, bool IsOfficialDocumentation) : ResourceDTO(Id, Title, Description, Url, AverageRating, Deprecated);

    public record ResourceCreateDTOServer
    {
        [Required]
        [StringLength(100)]
        public string TitleFromUser { get; set; }

        public string TitleFromSource { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime TimeOfReference { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string HostBaseUrl { get; set; }

        public ICollection<string> TagsFoundInSource { get; set; } = new List<string>();

        [Required]
        public int InitialRating { get; set; }

        [Required]
        public Boolean Deprecated { get; set; }

        [Required]
        public DateTime LastCheckedForDeprecation { get; set; }

        [Required]
        public bool IsVideo { get; set; }

        [Required]
        public bool IsOfficialDocumentation { get; set; }
    }

    public record ResourceCreateDTOClient
    {
        [Required]
        [StringLength(100)]
        public string Title { get; init; }

        [Required]
        public string UserId { get; init; }

        [Required]
        [StringLength(500)]
        public string Description { get; init; }
        
        [Required]
        public string Url { get; init; }

        [Required]
        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int InitialRating { get; init; }
    }

    public record ResourceUpdateDTO 
    {
        [Required]
        public int Id { get; init; }
        
        [Required]
        public Boolean Deprecated { get; init; }
    }
}