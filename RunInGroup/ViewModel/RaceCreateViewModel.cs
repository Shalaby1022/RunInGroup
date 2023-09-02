﻿using RunInGroup.Data.Enums;
using RunInGroup.Models;

namespace RunInGroup.ViewModel
{
    public class RaceCreateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}