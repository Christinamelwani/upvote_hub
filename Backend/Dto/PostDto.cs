namespace Backend.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string? Link { get; set; }

        public string? ImageUrl { get; set; }
        public int VoteNookId { get; set; }
    }
}