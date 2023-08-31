using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;

namespace RunInGroup.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly RunDbContext _context;

        public ClubController(RunDbContext context, IClubRepository clubRepository)
        {
            _context = context;
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _clubRepository.GetAllClubsAsync();
            return View(clubs);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            var clubs = await _clubRepository.GetClubByIdAsync(id);
            return View(clubs);
        }
    }
}
