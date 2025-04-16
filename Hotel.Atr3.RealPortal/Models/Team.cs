using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Atr3.RealPortal.Models
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

        //[NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + MiddleName;
            }
        }

        public string AboutTeam { get; set; }
		public byte[] ImagePath { get; set; }

		public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
