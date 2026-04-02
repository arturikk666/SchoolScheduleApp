namespace _111.Models
{
    public class Replacement
    {
        public int Id { get; set; }
        public int AbsenceId { get; set; }
        public int SubstituteTeacherId { get; set; }
        public string Status { get; set; }
        public Absence Absence { get; set; }
        public Teacher SubstituteTeacher { get; set; }
    }
}