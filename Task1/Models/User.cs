using System;

namespace Task1.Models
{
    public class User
    {
        public int id { get; set; }
        public int? teamId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public DateTime registeredAt { get; set; }
        public DateTime birthDay { get; set; }
    }
}
