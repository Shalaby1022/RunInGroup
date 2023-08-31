using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;

namespace RunInGroup.Controllers
{
    public class RaceController : Controller
    {
        private readonly RunDbContext _context;
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository, RunDbContext context)
        {
            _raceRepository = raceRepository;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var races = await _raceRepository.GetAllRacesAsync();
            return View(races);
        }

        public async Task<IActionResult> Details(int id)
        {
            var races = await _raceRepository.GetRaceByIdAsync(id);
            return View(races);
        }

    }
}
