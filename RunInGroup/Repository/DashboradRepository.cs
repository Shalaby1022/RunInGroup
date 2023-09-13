using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;

namespace RunInGroup.Repository
{
    public class DashboradRepository : IDashboardRepository
    {
        private readonly RunDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public DashboradRepository(RunDbContext context , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
        }

        public bool save()
        {
            _context.SaveChanges();
            return true;
        }

        public bool Update(AppUser appUser)
        {
            _context.Update(appUser);
            return save();

        }
        public async Task<AppUser> GetClubByIdAsyncNoTracking(string id)
        {
            var users =  await _context.Users.AsNoTracking().FirstOrDefaultAsync();
            return users;

        }

        public async Task<AppUser> GetUserId(string id)
        {
            var users = await _context.Users.FindAsync(id);
            return users;

        }

        async Task<List<Club>> IDashboardRepository.GetAllClubsAsync()
        {
            var CurUSER = _contextAccessor.HttpContext?.User.GetUserId();
            var Userclubs = _context.Clubs.Where(r => r.AppUser.Id == CurUSER);

            return await Userclubs.ToListAsync();
        }

        async Task<List<Race>> IDashboardRepository.GetAllRacesAsync()
        {
            var CurUser = _contextAccessor.HttpContext?.User.GetUserId();
            var userraces = _context.Races.Where(r => r.AppUser.Id == CurUser);

            return await userraces.ToListAsync();
        }

    }
}
