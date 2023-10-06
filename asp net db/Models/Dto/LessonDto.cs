using System.ComponentModel.DataAnnotations;

namespace asp_net_db.Models.Dto
{
    public class LessonDto
    {
        [Required]
        public string Title { get; set; }
        [Required, Display(Name = "Словесное описание занятия")]
        public string Description { get; set; }
        [Required, Display(Name = "Тип занятия (тест, урок, лекция)")]
        public string LessonType { get; set; }
        [Display(Name = "Время конца прохождения (необяз)")]
        public long? endTime { get; set; }
        [Required, Display(Name = "Перепрохождение изменит оценку?")]
        public bool canRetry { get; set; }
        [Required, Display(Name = "Минимальная оценка для зачета (-1 если лекция)")]
        public int minScore { get; set; }
        public string Script { get; set; }
    }
}
