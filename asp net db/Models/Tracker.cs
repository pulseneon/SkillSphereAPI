namespace asp_net_db.Models
{
    public class Tracker: BaseModel
    {
        public int LessonId { get; set; }
        public int UserId { get; set; }
        public int ScoreOf100 { get; set; }
        public double ScoreOf5 { get; set; }
        public long createdAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public int RetryCount { get; set; } = 1;
    }
}
