using RunInGroup.Models;

namespace RunInGroup.Data.Interface
{
    public interface IDashboardRepository
    {
        public Task<List<Club>> GetAllClubsAsync();
        public Task<List<Race>> GetAllRacesAsync();  

    }
}
