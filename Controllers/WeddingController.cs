using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Data;
using WeddingPlanner.Models;
using WeddingPlanner.ViewModels;

namespace WeddingPlanner.Controllers
{
    [Route ("weddings")]
    public class WeddingController : Controller
    {
        private readonly DataContext _context;

        public WeddingController (DataContext context)
        {
            _context = context;
        }

        [HttpGet ("dashboard")]
        public IActionResult Index ()
        {
            int? userId = HttpContext.Session.GetInt32 ("userId");
            if (userId is null)
                return RedirectToAction ("Index", "Auth");

            List<Wedding> allWeddings = _context.Weddings
                .Include (w => w.Guests)
                .ThenInclude (g => g.Attending)
                .ToList ();

            string userName = HttpContext.Session.GetString ("userName");
            ViewBag.User = _context.Users.Where (u => u.UserId == userId).FirstOrDefault ();
            ViewBag.UserName = userName;
            ViewBag.Weddings = allWeddings;
            return View ();
        }

        [HttpGet ("add")]
        public IActionResult AddWedding ()
        {
            int? userId = HttpContext.Session.GetInt32 ("userId");
            if (userId is null)
                return RedirectToAction ("Index", "Auth");

            return View ();
        }

        [HttpPost ("add")]
        public IActionResult NewWedding (AddWeddingViewModel wedding)
        {
            int? userId = HttpContext.Session.GetInt32 ("userId");

            if (!ModelState.IsValid)
                return View ("AddWedding");

            Wedding newWedding = new Wedding
            {
                WedderOne = wedding.WedderOne,
                WedderTwo = wedding.WedderTwo,
                Date = wedding.Date,
                UserId = (int) userId
            };

            _context.Weddings.Add (newWedding);
            _context.SaveChanges ();

            return RedirectToAction ("Index");
        }

        [HttpGet ("rsvp/{weddingId}")]
        public IActionResult RVSP (int weddingId)
        {
            int? userId = HttpContext.Session.GetInt32 ("userId");

            User userToJoin = _context.Users
                .FirstOrDefault (u => u.UserId == userId);

            Wedding weddingToJoin = _context.Weddings
                .Include (g => g.Guests)
                .FirstOrDefault (w => w.WeddingId == weddingId);

            Guest newGuest = new Guest
            {
                AttendingId = (int) userId,
                WeddingId = weddingId,
                Attending = userToJoin,
                Wedding = weddingToJoin
            };

            _context.Guests.Add (newGuest);
            _context.SaveChanges ();

            return RedirectToAction ("Index");
        }

        [HttpGet ("unrsvp/{weddingId}")]
        public IActionResult UnRsvp (int weddingId)
        {
            int? userId = HttpContext.Session.GetInt32 ("userId");
            Guest rsvp = _context.Guests
                .FirstOrDefault (g => g.WeddingId == weddingId && g.AttendingId == userId);

            _context.Remove(rsvp);
            _context.SaveChanges();

            return RedirectToAction ("Index");
        }
    }
}