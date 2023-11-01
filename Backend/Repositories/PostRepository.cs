using Backend.Interfaces;
using Backend.Models;
using Backend.Data;

namespace Backend.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Post> GetPosts()
        {
            return _context.Posts
                .OrderByDescending(p => p.Votes.Sum(v => v.Up ? 1 : -1))
                .ToList();
        }

        public bool CreatePost(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
