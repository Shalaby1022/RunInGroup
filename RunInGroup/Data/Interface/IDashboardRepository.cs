using RunInGroup.Models;

namespace RunInGroup.Data.Interface
{
    public interface IDashboardRepository
    {
        public Task<List<Club>> GetAllClubsAsync();
        public Task<List<Race>> GetAllRacesAsync();
        public Task<AppUser> GetUserId(string id);
        public Task<AppUser> GetClubByIdAsyncNoTracking(string id);

        bool Update(AppUser appUser);
        bool save();



    }
}
