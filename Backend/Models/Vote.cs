namespace Backend.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public bool Up { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
