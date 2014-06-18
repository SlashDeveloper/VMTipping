using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VMTipping.Model;

namespace VMTippekonkurranse.Models
{
    public class Game
    {
       
        public int Id { get; set; }

        [InverseProperty("Id")]
        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }
        public int HomeTeamId { get; set; }

        [InverseProperty("Id")]
        [ForeignKey("AwayTeamId")]
        public virtual Team AwayTeam { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public DateTime? Date { get; set; }

        [NotMapped]
        public bool IsPlayed {
            get
            {
                if (!Date.HasValue)
                {
                    return false;
                }
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cetZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime cetTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cetZone);
                if (Date.Value <= cetTime)
                {
                    return true;
                }
                return false;
            } 
        }
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