using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Atr3.RealApi.Model
{
    [Table("atr3Team")]
    public class Team
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "admin";

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string AboutTeam { get; set; }
        public byte[]? ImagePath { get; set; } = null;

        public int PositionId { get; set; }
        public Position? Position { get; set; } = null;
    }
}
