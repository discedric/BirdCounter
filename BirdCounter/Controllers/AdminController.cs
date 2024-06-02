using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;
using BirdCounter.Model;
using Microsoft.EntityFrameworkCore;

namespace BirdCounter.Controllers
{
	public class AdminController : Controller
	{
		private readonly BirdService _birdService;
		private readonly CountingSessionService _countingSessionService;
		private readonly UserService _userService;
		public AdminController(BirdService birdService, CountingSessionService countingSessionService, UserService userService)
		{
			_birdService = birdService;
			_countingSessionService = countingSessionService;
			_userService = userService;
		}

		[HttpGet]
		[Route("/Admin")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult BirdControl()
		{
			var birds = _birdService.Find();
			return View(birds);
		}
		[HttpGet]
		public IActionResult AddBird()
        {
			
            return View();
        }
		[HttpGet]
		public IActionResult EditBird(int id)
		{
			return View(_birdService.Find(id));
		}
		[HttpGet]
		public IActionResult CountSessions()
		{
			return View(_countingSessionService.Find().ToList());
		}
		[HttpGet]
		public IActionResult DetailsSession(int id)
        {
            ViewBag.Birds = _birdService.Find();
            return View(_countingSessionService.Find(id));
        }
		[HttpGet]
		public IActionResult AddCountSession()
        {
            return View();
        }
		[HttpGet]
		public IActionResult EditCountSession(int id)
        {
			ViewBag.Birds = _birdService.Find();
			ViewBag.Users = _userService.Find();
			var countingSession = _countingSessionService.Find(id);
            return View(countingSession);
        }
		[HttpPost]
		public IActionResult AddBird(Bird bird)
        {
            bird.ImagePath = "/Images/" + bird.ImagePath;
            _birdService.Add(bird);
            return RedirectToAction("BirdControl");
        }
		[HttpPost]
		public IActionResult AddCountSession(CountingSession countingSession)
        {
            _countingSessionService.Add(countingSession);
            return RedirectToAction("CountSessions");
        }
		[HttpPost]
		public IActionResult UpdateBird(Bird bird)
        {
            _birdService.Update(bird);
            return RedirectToAction("BirdControl");
        }
		[HttpPost]
		public IActionResult UpdateCountSession(CountingSession countingSession)
        {
			countingSession.User = _userService.Find(countingSession.User.Id);
			ModelState.Remove("User.Name");
            if (ModelState.IsValid)
            {
                _countingSessionService.Update(countingSession);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Birds = _birdService.Find();
            ViewBag.Users = _userService.Find();
            return View("EditCountSession", countingSession);
        }
		[HttpPost]
		public IActionResult DeleteBird(int id)
        {
            _birdService.Delete(id);
            return RedirectToAction("BirdControl");
        }
		[HttpPost]
		public IActionResult DeleteCountSession(int id)
        {
            _countingSessionService.Delete(id);
            return RedirectToAction("CountSessions");
        }
	}
}
