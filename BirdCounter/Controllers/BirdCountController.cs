using BirdCounter.Model;
using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;

namespace BirdCounter.Controllers
{
    public class BirdCountController : Controller
    {
        private readonly BirdCountService _birdCountService;
        private readonly CountingSessionService _countingSessionService;

        public BirdCountController(BirdCountService birdCountService, CountingSessionService countingSessionService)
        {
            _birdCountService = birdCountService;
            _countingSessionService = countingSessionService;
        }



        [HttpPost]
        public IActionResult CountBird(int birdId, CountingSession Csession)
        {
            var session = _countingSessionService.Find(Csession);
            if (session == null)
            {
                return NotFound("Counting session not found.");
            }

            var birdCount = _birdCountService.GetBirdCountForSession(birdId, Csession);
            if (birdCount == null)
            {
                birdCount = new BirdCount
                {
                    Id = birdId,
                    CountingSession = Csession,
                    Count = 0
                };
            }

            birdCount.Count++;
            _birdCountService.Update(birdCount, Csession);

            return RedirectToAction("Details", "CountingSession", new { id = Csession.Id });
        }
    }
}
}
