using RunInGroup.Models;

namespace RunInGroup.Data.Interface
{
    public interface IUserRepository
    {
        public Task<IEnumerable<AppUser>> GetAllAppUser();
        public Task<AppUser> GetUserId(string id);
        public bool Add(AppUser appUser);
        public bool Delete(AppUser appUser);
        public  bool Update(AppUser appUser);
        bool save();


    }
}
