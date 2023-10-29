namespace Backend.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }

        public string ImageUrl { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public VoteNook VoteNook { get; set; }
        public int VoteNookId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
