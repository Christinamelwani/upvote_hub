using Backend.Models;

namespace Backend.Interfaces
{
    public interface IVoteRepository
    {
        bool CreateVote(Vote vote);
        bool Save();
    }
}
