using System.ComponentModel.DataAnnotations;

namespace asp_net_db.Models
{
    public class Test
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }
        public int Count { get; set; }
    }
}
