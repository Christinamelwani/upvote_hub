using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPost(int id);
        ICollection<Post> SearchForPosts(string query);
        Post GetPostByTitle(string title);
        bool CreatePost(Post post);
        bool Save();
    }
}
