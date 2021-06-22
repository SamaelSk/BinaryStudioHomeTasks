using System;

namespace Task1.Models
{
    public class ProjectTask
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int performerId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int state { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? finishedAt { get; set; }
    }
}
