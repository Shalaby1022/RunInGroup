using RunInGroup.Models;

namespace RunInGroup.Data.Interface
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAllRacesAsync();
        Task<Race> GetRaceByIdAsync(int id);
        Task<Race> GetRaceByIdAsyncNoTracking(int id);

        Task<IEnumerable<Race>> GetRacesByCityAsync(string city);

        bool Add(Race race);
        bool Delete(Race race);
        bool Update(Race race);
        bool SaveChanges();

    }
}
