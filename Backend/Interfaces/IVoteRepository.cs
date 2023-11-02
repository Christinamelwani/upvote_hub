using Backend.Models;

namespace Backend.Interfaces
{
    public interface IVoteRepository
    {
        Vote GetVote(int VoteId);
        Vote GetVoteByUserAndPost(int userId, int postId);
        bool CreateVote(Vote vote);
        bool Save();
        bool VoteExists(int userId, int postId);
        bool DeleteVote(Vote vote);
    }
}
