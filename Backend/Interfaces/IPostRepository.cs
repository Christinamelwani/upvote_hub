using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPostByTitle(string title);
        bool CreatePost(Post post);
        bool Save();
    }
}
