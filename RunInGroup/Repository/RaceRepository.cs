using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;

namespace RunInGroup.Repository
{
    public class RaceRepository :IRaceRepository
    {
        private readonly RunDbContext _context;

        public RaceRepository(RunDbContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            _context.Add(race);
            return SaveChanges();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return SaveChanges();
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            return await _context.Races.Include(c=>c.Address).ToListAsync();  
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null; // Or you can throw an exception here.
            }

            var race = await _context.Races.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
            return race;
        }


        public async Task<IEnumerable<Race>> GetRacesByCityAsync(string city)
        {
            return await _context.Races.Include(c=>c.Address).Where(c=>c.Address.City == city).ToListAsync();
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;

        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return SaveChanges();
        }
    }
}
