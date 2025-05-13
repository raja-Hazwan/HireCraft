using System;

namespace HireCraft.Backend.Models
{
    public class Education
    {
        public int Id { get; set; }                // PK
        public Guid ResumeId { get; set; }         // FK
        public Resume Resume { get; set; } = null!;
        public string Institution { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
