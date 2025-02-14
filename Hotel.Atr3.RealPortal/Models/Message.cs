using System.ComponentModel.DataAnnotations;

namespace Hotel.Atr3.RealPortal.Models
{
    public class Message
    {
        [Required(ErrorMessage = "Поле Name обязательно к заполнению")]
        public string name { get; set; }


        public string email { get; set; }       
        
        [Required(ErrorMessage = "Поле email обязательно к заполнению")]
        [EmailAddress(ErrorMessage = "Указан не корректный email адрес")]
        public string message { get; set; }

        [CustomDate]
        public DateTime createDate { get; set; }

        [NameValidate(NotAllowed = new string[] { "Шымкент" },
            ErrorMessage = "Отправлять форму с города Шымкент нельзя")]
        public string city { get; set; }
    }



    //public class Message
    //{
    //    [Required(ErrorMessage ="Поле name обязательно к заполнению")]
    //    public string name { get; set; }

    //    public string email { get; set; }
    //    public string message { get; set; }
    //    public DateTime createDate { get; set; }
    //    public string city { get; set; }
    //}
}