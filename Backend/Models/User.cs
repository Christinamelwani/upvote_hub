namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<VoteNook> CreatedVoteNooks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
