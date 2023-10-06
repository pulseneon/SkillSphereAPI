namespace asp_net_db.Models
{
    public class TeachersStats
    {
        public int PercentageCompletedCourses { get; set; }
        public int CoursesCount { get; set; }
        public int LessonsCount { get; set; }
        public int HomeworkCount { get; set; }
        public int PercentageCompletedHomework { get; set; }
        public float PopularMark { get; set; }
    }
}
