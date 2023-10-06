using System.Linq;

namespace asp_net_db.Models
{
    public class AllTeacherStatsCourcesModel
    {
        public double min { get; set; }
        public double max { get; set; }
        public double middle { get; set; }
        public double median { get; set; }
        public double? mode { get; set; }

        public AllTeacherStatsCourcesModel(List<double> marks)
        {
            if (marks.Count == 0)
                return;

            marks.Sort();
            marks.ToArray();

            min = marks.Min();
            max = marks.Max();
            middle = marks.Average();

            if (marks.Count % 2 == 0)
            {
                var middleIndex = marks.Count / 2;
                var middleValue1 = marks[middleIndex - 1];
                var middleValue2 = marks[middleIndex];
                middle = (middleValue1 + middleValue2) / 2.0;
            }
            else
            {
                var middleIndex = marks.Count / 2;
                median = marks[middleIndex];
            }

            mode = marks.GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();
        }
    }
}
