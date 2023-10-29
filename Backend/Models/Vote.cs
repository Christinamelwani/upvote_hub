namespace Backend.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool Up { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
