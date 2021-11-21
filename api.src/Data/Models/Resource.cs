using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    [Index(nameof(Url), IsUnique = true)] 
    public class Resource 
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime TimeOfReference { get; set; }

        public DateTime TimeOfResourcePublication { get; set; }

        //public int AvaredgeRating { get; set; }

        [Required]
        public string Url { get; set; }

        public ICollection<Tag> Tags { get; set; }
        
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public Boolean Deprecated { get; set; } = false;

        [Required]
        public DateTime LastCheckedForDeprecation { get; set; }

    }
}