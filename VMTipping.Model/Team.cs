﻿using System.Collections.Generic;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Game> HomeMatches { get; set; }
        public virtual ICollection<Game> AwayMatches { get; set; }
    }
}