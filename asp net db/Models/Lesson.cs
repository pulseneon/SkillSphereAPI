namespace asp_net_db.Models
{
    public class Lesson: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LessonType { get; set; } // лекция или тест
        public long? endTime { get; set; } // время конца сдачи лекции
        public bool canRetry { get; set; } // можно ли перепройти
        public int minScore { get; set; } // минимальная оценка для зачета ????
        public long createdAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public bool isDeleted { get; set; } = false;
        public string Script { get; set; }
    }
}
