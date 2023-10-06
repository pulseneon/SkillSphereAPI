namespace asp_net_db.Models
{
    public class Content: BaseModel
    {
        public string Title { get; set; }
        public string Type { get; set; } // видео или материал
        public string Url { get; set; }
    }
}
