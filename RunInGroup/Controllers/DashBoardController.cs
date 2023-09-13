using Microsoft.AspNetCore.Mvc;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;
using RunInGroup.ViewModel;
using System.Collections.Immutable;

namespace RunInGroup.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService photoService;
        private readonly RunDbContext _context;


        public DashBoardController(RunDbContext context , IDashboardRepository dashboardRepository , IHttpContextAccessor httpContextAccessor  , IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            this.photoService = photoService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllRacesAsync();
            var userClubs = await _dashboardRepository.GetAllClubsAsync();

            var dashboardviewmodel = new DashboardViewModel()
            {
                races = userRaces,
                clubs = userClubs,
            };

            return View(dashboardviewmodel);
        }
       
        public async Task<IActionResult> EditUserDashboardProfile(string id)
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserId(curUserId);
            if (user == null) return View("Error");
           
    
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> EditUserDashboardProfile(EditUserDashboardProfile editUserDashboardProfile)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "failed to edit profile ");

                return View("editUserDashboardProfile", "Error");
            }
            var appuser = await _dashboardRepository.GetClubByIdAsyncNoTracking(editUserDashboardProfile.Id);
            if (appuser == null) return View("Error");
           

           

            return View(editUserDashboardProfile);


        }
    }
}
