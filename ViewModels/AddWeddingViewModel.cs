using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.ViewModels
{
    public class AddWeddingViewModel
    {   
        [Required]
        public string WedderOne { get; set; }
        [Required]
        public string WedderTwo { get; set; }
        [Required]
        [DataType (DataType.Date)]
        public DateTime Date { get; set; }
    }
}