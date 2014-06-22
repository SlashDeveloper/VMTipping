using System;

namespace VMTipping.Model
{
    public class RoundTeam
    {
        public int Id { get; set; }
        public Round Round { get; set; }
        public int RoundId { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public int Rank { get; set; }
        public DateTime DateTimeQualified { get; set; }
    }
}