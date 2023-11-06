using Backend.Models;

namespace Backend.Interfaces
{
    public interface IVoteNookRepository
    {
        ICollection<VoteNook> GetVoteNooks();
        VoteNook GetVoteNook(int id);
        VoteNook GetVoteNookByName(string name);
        ICollection<Post> GetPostsInVoteNook(int id);
        bool CreateVoteNook(VoteNook voteNook);
        bool Save();
    }
}
