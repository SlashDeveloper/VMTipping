using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<RoundTeam> TeamsInRound { get; set; }
        public DateTime? StartActive { get; set; }
        public DateTime? EndActive { get; set; }
        public int PointPerCorrectTeam { get; set; }
        public bool IsRanked { get; set; }
        [NotMapped]
        public bool IsActive {
            get
            {
                if (!(StartActive.HasValue && EndActive.HasValue))
                {
                    return false;
                }
                var cetDate = CETDateHelper.GetCurrentCETDateTime();
                return StartActive.Value.Date <= cetDate.Date && EndActive.Value >= cetDate.Date;
            } }
    }
}
