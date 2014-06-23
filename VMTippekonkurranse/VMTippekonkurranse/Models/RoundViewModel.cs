using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMTipping.Model;

namespace VMTippekonkurranse.Models
{
    public class RoundViewModel
    {
        public Round Round;
        public int NumTeams { get; set; }
        public IList<RoundTeam> RoundTeams; 
        public IList<RoundUserScore> RoundUserScores;

        public string CollapsibleId
        {
            get { return "round" + Round.Id + "details"; }
        }
        public string HideStartButtonId
        {
            get { return "hidestartbutton" + Round.Id ; }
        }
        public string HideLastButtonId
        {
            get { return "hidelastbutton" + Round.Id;  }
        }
        public string CollapseOpen
        {
            get
            {
                if (Round.IsActive)
                {
                    return "in";
                }
                return "";
            }
        }
    }

}