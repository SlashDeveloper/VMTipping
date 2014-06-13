using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMTippekonkurranse.Models
{
    public class Lag
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public int GruppeId { get; set; }
    }
}