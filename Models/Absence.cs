using System;

namespace _111.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public DateTime AbsenceDate { get; set; }
        public string Reason { get; set; }
        public Teacher Teacher { get; set; }
    }
}