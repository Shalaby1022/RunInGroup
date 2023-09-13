using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunInGroup.Data.Interface;
using RunInGroup.Helpers;
using RunInGroup.Models;
using RunInGroup.ViewModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;

namespace RunInGroup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger , IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipinfo = new IpInfo();
            var homeviewmodel = new HomeIpViewModel();

            try
            {
                string url = "https://ipinfo.io/154.182.213.7?token=d43addbf470df7";
                var info = new WebClient().DownloadString(url);
                ipinfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo regionInfo = new RegionInfo(ipinfo.Country);
                ipinfo.Country = regionInfo.EnglishName;

                homeviewmodel.City = ipinfo.City;
                homeviewmodel.State = ipinfo.Region;

                if (homeviewmodel.City != null)
                {
                    homeviewmodel.clubs = await _clubRepository.GetAllClubsAsync();

                }
                else
                {
                    homeviewmodel.clubs = null;
                }

                return View(homeviewmodel);

            }
            catch(Exception ex)
            {
                homeviewmodel.clubs = null;

            }

                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}