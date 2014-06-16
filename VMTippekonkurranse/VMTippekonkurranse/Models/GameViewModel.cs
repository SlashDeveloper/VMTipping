using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMTipping.Model;

namespace VMTippekonkurranse.Models
{
    public class GameViewModel
    {
        public Game Game;
        public IList<GameUserScore> GameUserScores;

        public string CollapsibleId
        {
            get { return "match" + Game.Id + "details"; }
        }

        public string CollapseOpen
        {
            get
            {
                if (Game.Date.HasValue && Game.Date.Value.Date == DateTime.Now.Date)
                {
                    return "in";
                }
                return "";
            }
        }

        public string DateOrTimeString
        {
            get
            {
                if (!Game.Date.HasValue)
                {
                    return "";
                }
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cetZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                DateTime cetTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cetZone);
                if (Game.Date.Value.Date == cetTime.Date)
                {
                    return Game.Date.Value.ToShortTimeString();
                }
                else
                {
                    return Game.Date.Value.ToShortDateString();
                }
            }
        }
    }

}