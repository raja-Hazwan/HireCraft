using System;

namespace HireCraft.Backend.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = null!;
    }
}
