using Backend.Models;

namespace Backend.Interfaces
{
    public interface IVoteNookRepository
    {
        VoteNook GetVoteNook(int id);
        bool CreateVoteNook(VoteNook voteNook);
        bool Save();
    }
}
