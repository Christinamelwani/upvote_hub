using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        ICollection<Post> SearchForPosts(string query);
        Post GetPostByTitle(string title);
        bool CreatePost(Post post);
        bool Save();
    }
}
