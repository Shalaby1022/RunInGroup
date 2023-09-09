using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Services;
using RunInGroup.Data;
using RunInGroup.Data.Interface;
using RunInGroup.Models;
using RunInGroup.Repository;
using RunInGroup.ViewModel;
using System.Diagnostics;

namespace RunInGroup.Controllers
{
    public class RaceController : Controller
    {
        private readonly RunDbContext _context;
        private readonly IPhotoService _raceService;
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository, RunDbContext context, IPhotoService raceService)
        {
            _raceRepository = raceRepository;
            _context = context;
            _raceService = raceService;
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

        [HttpGet]
        public IActionResult Create()
        {

            var raceCreateViewModel = new RaceCreateViewModel();
            return View(raceCreateViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(RaceCreateViewModel RaceVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _raceService.AddPhototAsync(RaceVm.Image);

                    if (result != null)
                    {
                        var race = new Race
                        {
                            Title = RaceVm.Title,
                            Description = RaceVm.Description,
                            Image = result.Url.ToString(),
                            Address = new Address
                            {
                                City = RaceVm.Address.City,
                                State = RaceVm.Address.State,
                                Street = RaceVm.Address.Street,
                            }
                        };

                        _raceRepository.Add(race);
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

                    ModelState.AddModelError("", "An error occurred while processing the request.");
                }
            }

            return View(RaceVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetRaceByIdAsync(id);
            if (id == null)
            {
                return View("Error");
            }

            var raceVm = new RaceEditViewModel
            {
                Title = race.Title,
                Description = race.Description,
                Address = race.Address,
                URL = race.Image,
                AddressId = race.AddressId,
                RaceCategory = race.RaceCategory
            };
            return View(raceVm);
        }

        [HttpPost]

        public async Task<IActionResult> Edit( int id , RaceEditViewModel raceEdit)
        {
            if(!ModelState.IsValid)
            
            {
                ModelState.AddModelError("", "can't edit the race");
                return View("Edit" , raceEdit);
            }
           var Userrace = await _raceRepository.GetRaceByIdAsyncNoTracking(id);

            if (Userrace == null)
            {
                return View("Error");
            }
            var photoResult = await _raceService.AddPhototAsync(raceEdit.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(raceEdit);
            }

            if (!string.IsNullOrEmpty(Userrace.Image))
            {
                _ = _raceService.DeletePhotoAsync(Userrace.Image);
            }

            var race = new Race
            {
                Id = id,
                Title = raceEdit.Title,
                Description = raceEdit.Description,
                Image = photoResult.Url.ToString(),
                AddressId = raceEdit.AddressId,
                Address = raceEdit.Address,
            };

            _raceRepository.Update(race);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var DeleteUser = await _raceRepository.GetRaceByIdAsync(id);
            if(DeleteUser == null)
            {
                return View("Error");
            }
            return View(DeleteUser);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var DeleteUser = await _raceRepository.GetRaceByIdAsync(id);
            if(DeleteUser == null) 
            {
                return View("Error");   
            }
             _raceRepository.Delete(DeleteUser);
            
            return RedirectToAction("Index");

        }
    }
    }
