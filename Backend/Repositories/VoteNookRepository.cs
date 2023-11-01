using Backend.Interfaces;
using Backend.Models;
using Backend.Data;

namespace Backend.Repositories
{
    public class VoteNookRepository : IVoteNookRepository
    {
        private readonly DataContext _context;

        public VoteNookRepository(DataContext context)
        {
            _context = context;
        }

        public VoteNook GetVoteNook(int id)
        {
            return _context.VoteNooks.Where(v => v.Id == id).FirstOrDefault();
        }


        public bool CreateVoteNook(VoteNook voteNook)
        {
            _context.Add(voteNook);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
