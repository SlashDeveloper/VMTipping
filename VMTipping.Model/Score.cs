using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMTipping.Model
{
    public class Score
    {
        public int Ranking { get; set; }
        public User User { get; set; }
        public int Points { get; set; }
    }
}
