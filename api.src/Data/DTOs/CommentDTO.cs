using Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.src.Data.DTOs
{
    public record CommentSimpleDTO 
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }

    public record CommentDetailsDTO
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Resource Resource { get; set; }

        [Required]
        public DateTime TimeOfComment { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }
}
