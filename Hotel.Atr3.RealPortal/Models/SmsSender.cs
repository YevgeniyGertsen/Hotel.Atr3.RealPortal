using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Hotel.Atr3.RealPortal.Models
{
    public class SmsSender : IMessage
    {
        public bool SendMessage(string to, string messageBody, string subject)
        {
            const string accountSid = "";
            const string authToken = "";

            TwilioClient.Init(accountSid, authToken);
            try
            {
                var message = MessageResource.Create(
                        body: messageBody,
                        from: new PhoneNumber("+18126136069"),
                        to: new PhoneNumber(to));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
