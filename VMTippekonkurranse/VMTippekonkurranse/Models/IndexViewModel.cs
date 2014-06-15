using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMTippekonkurranse.Models
{
    public class IndexViewModel
    {
        public IList<GameViewModel> TodaysGames;
        public IList<GameViewModel> EarlierGames;
        public IList<GameViewModel> UpcomingGames;
    }
}