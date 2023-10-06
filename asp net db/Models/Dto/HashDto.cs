namespace asp_net_db.Models.Dto
{
    public class HashDto
    {
        public string Hash { get; set; }

        public HashDto(string hash)
        {
            Hash = hash;
        }
    }
}
