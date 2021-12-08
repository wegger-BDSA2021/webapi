using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record CommentDTO 
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ResourceId { get; set; }

        public DateTime TimeOfComment { get; set; }

        public string Content { get; set; }
    }

    public record CommentDetailsDTO
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Resource Resource { get; set; }

        public int ResourceId { get; set; }

        public DateTime TimeOfComment { get; set; }

        public string Content { get; set; }
    }

    public record CommentCreateDTOServer
    {
        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        public Resource Resource { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        public DateTime TimeOfComment { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }

    public record CommentCreateDTOClient
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }

    public record CommentUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public User? User { get; set; }

        public Resource? Resource { get; set; }

        public DateTime? TimeOfComment { get; set; }

        [StringLength(300)]
        public string? Content { get; set; }
    }
}
