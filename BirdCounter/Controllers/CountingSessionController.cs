using BirdCounter.Model;
using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounter.Controllers
{
    public class CountingSessionController : Controller
    {
        private readonly CountingSessionService _countingSessionService;

        public CountingSessionController(CountingSessionService countingSessionService)
        {
            _countingSessionService = countingSessionService;
        }

        // TODO: Index, Create, Edit, Stop
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
        public IActionResult Create(CountingSession countingSession)
        {
            if (ModelState.IsValid)
            {
                _countingSessionService.Add(countingSession);
                return RedirectToAction("Index");
            }
            return View(countingSession);
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Stop(int id)
        {
            var session = _countingSessionService.Find(id);
            if (session == null)
            {
                return NotFound("Counting session not found.");
            }
            session.End = DateTime.Now;
            _countingSessionService.Update(session);
            return RedirectToAction("Details",  new { id = session.Id });
        }

        public IActionResult Details(int id)
        {
            return View(_countingSessionService.Find(id));
        }
    }
}
