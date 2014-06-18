using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMTippekonkurranse.Models
{
    public class CETDateHelper
    {
        public static DateTime GetCurrentCETDateTime()
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cetZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime cetTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cetZone);
            return cetTime;
        }
    }
}