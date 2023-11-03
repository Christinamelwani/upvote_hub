using Backend.Interfaces;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

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
            return _context.Posts.Include("Author").Include("Votes").OrderByDescending(p => p.Votes.Sum(v => v.Up ? 1 : -1)).ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Author)
                .Include(p => p.Votes)
                .Include(p => p.Author)
                .FirstOrDefault();
        }

        public ICollection<Post> SearchForPosts(string query)
        {
            return _context.Posts.Where(post => post.Title.Contains(query) || post.Text.Contains(query)).Include("Author").Include("Votes").ToList();
        }

        public Post GetPostByTitle(string title)
        {
            return _context.Posts.Where(p => p.Title == title).FirstOrDefault();
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
