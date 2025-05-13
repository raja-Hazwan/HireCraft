using System;
using System.Collections.Generic;

namespace HireCraft.Backend.Models
{
    public class Resume
    {
        public Guid Id { get; set; }               // PK
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}