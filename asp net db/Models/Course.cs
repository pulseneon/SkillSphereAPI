namespace asp_net_db.Models
{
    public class Course: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public List<int> StudentsIds { get; set; } = new List<int>();
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public List<Homework> Homeworks { get; set; } = new List<Homework>();
        public List<Content> Contents { get; set; } = new List<Content>();
        public string InviteHash { get; set; } = Guid.NewGuid().ToString();
        public long createdAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public bool isDeleted { get; set; } = false;
    }
}
