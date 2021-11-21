using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
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
        
        public ICollection<Rating> Ratings { get; set; }
        
        public ICollection<Comment> Comments { get; set; }

        [Required]
        public Boolean Deprecated { get; set; } = false;


        
    }
}