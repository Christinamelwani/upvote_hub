using Backend.Interfaces;
using Backend.Models;
using Backend.Data;

namespace Backend.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly DataContext _context;

        public VoteRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateVote(Vote vote)
        {
            _context.Add(vote);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
