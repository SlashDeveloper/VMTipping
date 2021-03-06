﻿using System;
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
                if (Game.Date.HasValue && Game.Date.Value.Date == CETDateHelper.GetCurrentCETDateTime().Date)
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
                var cetTime = CETDateHelper.GetCurrentCETDateTime();
                if (Game.Date.Value.Date == cetTime.Date)
                {
                    return Game.Date.Value.ToShortTimeString();
                }
                else
                {
                    return Game.Date.Value.Day + "." + Game.Date.Value.Month + " " + Game.Date.Value.ToShortTimeString();
                }
            }
        }
    }

}