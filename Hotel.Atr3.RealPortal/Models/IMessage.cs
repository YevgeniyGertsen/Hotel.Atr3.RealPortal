namespace Hotel.Atr3.RealPortal.Models
{
    public interface IMessage
    {
        public bool SendMessage(string to, string messageBody, string subject);
    }
}
