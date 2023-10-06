namespace asp_net_db.Models
{
    public class Homework: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Deadline { get; set; }
        public string Photo { get; set; }
        public long createdAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public bool isDeleted { get; set; } = false;
    }
}
