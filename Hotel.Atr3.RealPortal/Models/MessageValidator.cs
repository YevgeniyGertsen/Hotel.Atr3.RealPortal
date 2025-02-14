using FluentValidation;

namespace Hotel.Atr3.RealPortal.Models
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(r => r.message).NotEmpty();
        }
    }
}
