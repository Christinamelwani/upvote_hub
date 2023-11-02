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

        public Vote GetVote(int voteId)
        {
            return _context.Votes.Where(v => v.Id == voteId).FirstOrDefault();
        }
        public bool CreateVote(Vote vote)
        {
            _context.Add(vote);
            return Save();
        }

        public bool VoteExists(int userId, int postId)
        {
            return _context.Votes.Any(v => v.User.Id == userId && v.Post.Id == postId);
        }

        public bool DeleteVote(Vote vote)
        {
            _context.Remove(vote);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
