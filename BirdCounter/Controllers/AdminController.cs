using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounter.Controllers
{
	public class AdminController : Controller
	{
		private readonly BirdService _birdService;
		public AdminController(BirdService birdService)
		{
			_birdService = birdService;
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
			return View(_birdService.Find());
		}

		[HttpGet]
		public IActionResult CountSessions()
		{
			return View();
		}
	}
}
