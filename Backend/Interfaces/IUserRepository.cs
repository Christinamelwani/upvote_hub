using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        ICollection<Post> GetPostsByUser(int userId);
        bool CreateUser(User user);
        bool Save();
    }
}
