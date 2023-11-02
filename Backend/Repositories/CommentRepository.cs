using Backend.Models;
using Backend.Interfaces;
using Backend.Data;

namespace Backend.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Comment> GetComments()
        {
            return _context.Comments.ToList();
        }
        public Comment GetComment(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }
        public bool CreateComment(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}