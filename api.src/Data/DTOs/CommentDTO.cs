using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record CommentDTO(int Id, string UserId, int ResourceId, DateTime TimeOfComment, string Content);

    public record CommentDetailsDTO(int Id, string UserId, int ResourceId, DateTime TimeOfComment, string Content) : CommentDTO(Id, UserId, ResourceId, TimeOfComment, Content);

    /*public record CommentDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ResourceId { get; set; }
        public DateTime TimeOfComment { get; set; }
        public string Content { get; set; }
    }*/

    /*public record CommentDetailsDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ResourceId { get; set; }
        public DateTime TimeOfComment { get; set; }
        public string Content { get; set; }
    }*/

    public record CommentCreateDTOServer
    {
        [Required]
        public string UserId { get; set; }

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
        public string UserId { get; set; }

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

        public string? UserId { get; set; }

        public int? ResourceId { get; set; }

        public DateTime? TimeOfComment { get; set; }

        [StringLength(300)]
        public string? Content { get; set; }
    }
}
