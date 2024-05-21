using BirdCounter.Model;
using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounter.Controllers
{
	public class BirdController : Controller
	{
		private readonly BirdService _birdService;
		public BirdController(BirdService birdService)
		{
			_birdService = birdService;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Bird bird)
		{
			ModelState.Remove("ImagePath");
            if (ModelState.IsValid)
            {
                _birdService.Add(bird);
                return RedirectToAction("BirdControl", "Admin");
            }
            return View(bird);
		}

		[HttpGet]

		public IActionResult Edit(int id)
		{
			return View(_birdService.Find(id));
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Bird bird)
		{
            ModelState.Remove("ImagePath");
            if (ModelState.IsValid)
            {
                _birdService.Update(bird);
                return RedirectToAction("BirdControl", "Admin");
            }
            return View(bird);
        }

		[HttpGet]
		public IActionResult Delete(int id)
		{
			return RedirectToAction("BirdControl","Admin");
		}
	}
}
