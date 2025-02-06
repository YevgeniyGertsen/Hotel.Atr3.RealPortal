namespace Hotel.Atr3.RealPortal.Models
{
    public class TeamViewModel
    {
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public List<TeamLinks> TeamLinks { get; set; }
    }

    public class TeamLinks
    {
        public string URL { get; set; }
        public string LinkType { get; set; }
    }
}