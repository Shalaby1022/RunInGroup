using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;

using RunInGroup.ViewModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace RunInGroup.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly ILogger<ClubController> _logger;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService, ILogger<ClubController> logger)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;  //<<<<<---THIS ONE 
            _logger = logger;
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

        [HttpGet]
        public IActionResult Create()
        {

            var createClubViewModel = new CreateClubViewModel();
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _photoService.AddPhototAsync(clubVM.Image);

                    Debug.WriteLine($"Result URL: {result?.Url}");


                    // Log the result for debugging
                    _logger.LogInformation("Cloudinary Upload Result: {@Result}", result);

                    if (result != null)
                    {
                        var club = new Club
                        {
                            Title = clubVM.Title,
                            Description = clubVM.Description,
                            Image = result.Url.ToString(),
                            Address = new Address
                            {
                                City = clubVM.Address.City,
                                State = clubVM.Address.State,
                                Street = clubVM.Address.Street,
                            }
                        };
                        Debug.WriteLine($"Club Object: {club}");
                        _clubRepository.Add(club);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Photo upload failed. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging
                    // Log the exception for debugging
                    _logger.LogError(ex, "An error occurred while processing the request.");
                    ModelState.AddModelError("", "An error occurred while processing the request.");
                }
            }

            return View(clubVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetClubByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetClubByIdAsyncNoTracking(id);
                                             
                                              


            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhototAsync(clubVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(clubVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }

            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address,
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }

        [HttpGet]
         public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetClubByIdAsync(id);
            if(clubDetails == null)
            {
                return View("Error");
            }
            return View(clubDetails);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetClubByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }


    }
}


