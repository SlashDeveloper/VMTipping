using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VMTipping.Model;

namespace VMTippekonkurranse.Models
{
    public class Match
    {
       
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public bool IsPlayed { get; set; }
        [NotMapped]
        public string Result
        {
            get
            {
                if (HomeGoals > AwayGoals) return "H";
                if (HomeGoals == AwayGoals) return "U";
                return "B";
            }
        }
    }
}