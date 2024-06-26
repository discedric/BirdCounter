﻿using BirdCounter.Model;
using BirdCounter.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdCounter.Controllers
{
    public class SessionController : Controller
    {
        private readonly CountingSessionService _countingSessionService;
        private readonly BirdService _birdService;
        private readonly UserService _userService;
        public SessionController(CountingSessionService countingSessionService, BirdService birdService, UserService userService)
        {
            _countingSessionService = countingSessionService;
            _birdService = birdService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var sessions = _countingSessionService.Find();
            foreach (var session in sessions)
            {
                if (string.IsNullOrEmpty(session.Location))
                {
                    _countingSessionService.Delete(session.Id);
                }
            }
            return View(sessions.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Create a new CountingSession with default values
            var newSession = new CountingSession
            {
                Start = DateTime.Now,

                Location = "",
                User = _userService.Find()[0] // default to the first user, this should be modified as needed
            };

            // Add the new session to the context but don't save it yet
            _countingSessionService.Add(newSession);

            // Redirect to the Bird Counting page with the new session ID
            return RedirectToAction("CountBirds", new { id = newSession.Id });
        }

        [HttpGet]
        public IActionResult CountBirds(int id)
        {
            var countingSession = _countingSessionService.Find(id);

            ViewBag.Birds = _birdService.Find();
            return View(countingSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CountBirds(CountingSession countingSession)
        {
            foreach (var birdCount in countingSession.BirdCounts)
            {
                var prefix = $"BirdCounts[{countingSession.BirdCounts.IndexOf(birdCount)}].Bird.";

                ModelState.Remove($"{prefix}Description");
                ModelState.Remove($"{prefix}ImagePath");
            }
            if (ModelState.IsValid)
            {
                countingSession.End = DateTime.Now;
                _countingSessionService.Update(countingSession);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Birds = _birdService.Find();
            return View(countingSession);
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            _countingSessionService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Birds = _birdService.Find();
            var session = _countingSessionService.Find(id);
            if (session is null)
            {
                return RedirectToAction(nameof(Index));
            }
            if(string.IsNullOrEmpty(session.Location))
            {
                _countingSessionService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }
    }
}
