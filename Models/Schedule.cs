namespace _111.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string Subject { get; set; }
        public int TeacherId { get; set; }
        public int DayOfWeek { get; set; }
        public int LessonNumber { get; set; }

        // Навигационные свойства (ОБЯЗАТЕЛЬНО)
        public Class Class { get; set; }
        public Teacher Teacher { get; set; }
    }
}