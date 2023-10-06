namespace asp_net_db.Models
{
    public class CourseProgressModel
    {
        public int Completed { get; set; }
        public int All { get; set; }
        public int Procent { get; set; }

        public CourseProgressModel(int all, int completed)
        {
            All = all;
            Completed = completed;
            Procent = (completed/all) *100;
        }
    }
}
