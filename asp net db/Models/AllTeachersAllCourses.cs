namespace asp_net_db.Models
{
    public class AllTeachersAllCourses
    {
        public int Id { get; set; }
        public string TitleCourse { get; set; }
        public AllTeacherStatsCourcesModel Marks { get; set; }

        public AllTeachersAllCourses(int id, string titleCourse, AllTeacherStatsCourcesModel marks)
        {
            Id = id;
            TitleCourse = titleCourse;
            Marks = marks;
        }
    }
}
