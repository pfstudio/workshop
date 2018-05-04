using System;

namespace workshop.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StudentId { get; set; }
        public DateTime SignInTime { get; set; }
        public DateTime? SignOutTime { get; set; }
    }
}