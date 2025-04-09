using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Atr3.Admin.Models
{
	[Table("atr3Team")]
	public class Team
	{
		public int Id { get; set; }
		public DateTime CreateAt { get; set; }
		public string CreatedBy { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string AboutTeam { get; set; }
        public string ImagePath { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
