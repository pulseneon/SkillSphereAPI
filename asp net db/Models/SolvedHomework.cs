namespace asp_net_db.Models
{
    public class SolvedHomework: BaseModel
    {
        public int StudentId { get; set; }
        public string? Comment { get; set; } // комментарий школьника
        public double ScoreOf5 { get; set; } // оценка
        public bool isChecked { get; set; } = false; // проверено ли дз
        public string? CheckedComment { get; set; } // комментарий учителя
        public long createdAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public bool isDeleted { get; set; } = false;
    }
}
