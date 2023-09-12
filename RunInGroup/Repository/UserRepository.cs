using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;
using RunInGroup.ViewModel;
using System.Xml.Linq;

namespace RunInGroup.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RunDbContext _context;

        public UserRepository(RunDbContext context)
        {
            _context = context;
        }

        public bool save()
        {
            _context.SaveChanges();
            return true;
        }
        bool IUserRepository.Add(AppUser appUser)
        {
            _context.Users.Add(appUser);
            return save();

        }

        bool IUserRepository.Delete(AppUser appUser)
        {
            _context.Remove(appUser);
            return save();
        }

        async Task<IEnumerable<AppUser>> IUserRepository.GetAllAppUser()
        {
            return await _context.Users.ToListAsync();
        }

        async Task<AppUser> IUserRepository.GetUserId(string id)
        {
           
            var users = await _context.Users.FindAsync(id);
            return users;
        }



        bool IUserRepository.Update(AppUser appUser)
        {
            _context.Update(appUser);
            return save();

        }

      

      
    }
}
