using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int id);
        bool CreateComment(Comment comment);
        bool DeleteComment(Comment comment);
        bool Save();
    }
}