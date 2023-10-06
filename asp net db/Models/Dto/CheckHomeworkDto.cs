using System.ComponentModel.DataAnnotations;

namespace asp_net_db.Models.Dto
{
    public class CheckHomeworkDto
    {
        [Required]
        public double ScoreOf5 { get; set; } // оценка
        public string? CheckedComment { get; set; } // комментарий учителя
    }
}
