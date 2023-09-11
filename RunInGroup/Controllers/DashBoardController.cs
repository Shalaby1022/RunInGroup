using Microsoft.AspNetCore.Mvc;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.ViewModel;

namespace RunInGroup.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly RunDbContext _context;


        public DashBoardController(RunDbContext context , IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
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
    }
}
