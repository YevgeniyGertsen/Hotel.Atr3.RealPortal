using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Atr3.Admin.Models
{
    [Table("atr3Position")]
	public class Position
	{
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreatedBy { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}