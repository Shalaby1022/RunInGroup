using Microsoft.AspNetCore.Mvc;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace RunInGroup.Controllers
{
    public class UsersController : Controller
    {
        private readonly RunDbContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController(RunDbContext context , IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }


        
        public async Task<IActionResult> Index()
        {
            var usres = await _userRepository.GetAllAppUser();
            List<UsersViewModel> users = new List<UsersViewModel>();

            foreach (var item in usres)
            {
                var userviewmodel = new UsersViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Pace = item.Pace,
                    Mileage = item.Mileage
                };
                users.Add(userviewmodel);
            }
            return View(users);

        }

        public async Task<IActionResult> Details(string id)
        {
            // Retrieve the user based on the provided id
            var user = await _userRepository.GetUserId(id);

            if (user != null)
            {
                // Create a single user details view model
                var userdetailsviewmodel = new UsersDetailsViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage
                };

                // Return the view with the populated user details view model
                return View(userdetailsviewmodel);
            }

            // Handle the case where the user with the provided id was not found
            return NotFound(); // You can return a 404 page or handle it as needed.
        }



    }
}
