using Backend.Interfaces;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

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

        public ICollection<Post> GetPostsInVoteNook(int id)
        {
            return _context.Posts.Where(p => p.VoteNook.Id == id).Include("Author").ToList();
        }

        public bool CreateVoteNook(VoteNook voteNook)
        {
            _context.Add(voteNook);
            return Save();
        }

        public ICollection<VoteNook> GetVoteNooks()
        {
            return _context.VoteNooks.Include("Creator").ToList();
        }

        public VoteNook GetVoteNookByName(string name)
        {
            return _context.VoteNooks.Where(v => v.Name == name).FirstOrDefault();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
