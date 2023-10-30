using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        bool CreateUser(User user);
        bool Save();
    }
}
