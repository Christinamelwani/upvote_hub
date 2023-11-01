using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        bool CreatePost(Post post);
        bool Save();
    }
}
