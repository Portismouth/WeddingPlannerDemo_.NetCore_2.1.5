using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        public int WeddingId { get; set; }
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User Planner { get; set; }
        public List<Guest> Guests { get; set; }

        public Wedding()
        {
            Guests = new List<Guest>();
        }
    }
}