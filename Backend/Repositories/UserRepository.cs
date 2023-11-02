using Backend.Interfaces;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public ICollection<Post> GetPostsByUser(int userId)
        {
            return _context.Posts.Where(p => p.Author.Id == userId).ToList();
        }
        public ICollection<Comment> GetCommentsByUser(int userId)
        {
            return _context.Comments.Where(p => p.Author.Id == userId).Include("Post").ToList();
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
