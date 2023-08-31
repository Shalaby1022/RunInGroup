using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;

namespace RunInGroup.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly RunDbContext _context;

        public ClubRepository(RunDbContext context)
        {
            _context = context;
        }

        public bool Add(Club club)
        {
            _context.Add(club);
            return save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return save();
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await _context.Clubs.Include(c => c.Address).ToListAsync();
        }


        public async Task<IEnumerable<Club>> GetClubByCityAsync(string city)
        {
            return await _context.Clubs.Include(c=>c.Address).Where(c=>c.Address.City == city).ToListAsync();
        }



        public async Task<Club?> GetClubByIdAsync(int id)
        {
            if (id == 0) // I have leant this Tip to avoid operator overloading so i'm using "Is" not "=="
            {
                return null; 
            }

            var club = await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
            return club;
        }


        public bool save()
        {
            _context.SaveChanges();
            return true;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return save();

        }
    }
}
