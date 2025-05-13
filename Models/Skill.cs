namespace HireCraft.Backend.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ProficiencyLevel { get; set; } // e.g., 1–5
    }
}