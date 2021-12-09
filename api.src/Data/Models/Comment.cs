using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Comment
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        
        public int? UserId { get; set; }

        public Resource Resource { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [Required]
        public DateTime TimeOfComment { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }
}