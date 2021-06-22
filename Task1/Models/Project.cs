using System;

namespace Task1.Models
{
    public class Project
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public int? teamId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime deadline { get; set; }
        public DateTime createdAt { get; set; }
    }
}
