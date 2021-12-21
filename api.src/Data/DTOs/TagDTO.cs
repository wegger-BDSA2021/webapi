using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public record TagDTO(int Id, string Name);
    public record TagDetailsDTO(int Id, string Name,  IReadOnlyCollection<string> ResourcesNames);

    public record TagCreateDTO
    {
        [Required]
        public string Name { get; init; }
    }

    public record TagUpdateDTO 
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public String NewName { get; init; }
    }
}