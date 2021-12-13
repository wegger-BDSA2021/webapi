using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    [Index(nameof(Name), IsUnique = true)] 
    public class Tag 
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Resource> Resources { get; set; } 
    }
}