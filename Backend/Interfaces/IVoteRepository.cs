using Backend.Models;

namespace Backend.Interfaces
{
    public interface IVoteRepository
    {
        Vote GetVote(int VoteId);
        bool CreateVote(Vote vote);
        bool Save();
        bool VoteExists(int userId, int postId);
        bool DeleteVote(Vote vote);
    }
}
