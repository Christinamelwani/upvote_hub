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
            return _context.Posts.Include("Author").OrderByDescending(p => p.Votes.Sum(v => v.Up ? 1 : -1)).ToList();
        }

        public ICollection<Post> SearchForPosts(string query)
        {
            return _context.Posts.Where(post => post.Title.Contains(query) || post.Text.Contains(query)).Include("Author").ToList();
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
