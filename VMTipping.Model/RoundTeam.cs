namespace VMTipping.Model
{
    public class RoundTeam
    {
        public int Id { get; set; }
        public Round Round { get; set; }
        public Team Team { get; set; }
        public int Rank { get; set; }
    }
}