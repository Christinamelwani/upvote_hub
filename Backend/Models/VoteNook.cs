namespace Backend.Models
{
    public class VoteNook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }

        public User Creator { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
