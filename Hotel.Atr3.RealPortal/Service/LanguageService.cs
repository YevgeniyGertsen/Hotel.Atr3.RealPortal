using Hotel.Atr3.RealPortal.Models;
using Twilio.Rest.Api.V2010.Account.Call;

namespace Hotel.Atr3.RealPortal.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly AppDbContext _db;
        public LanguageService(AppDbContext db)
        {
            _db = db;
        }

        public Language GetLanguageByCulture(string culture)
        {
            return _db.Language
                .FirstOrDefault(f => f.Culture == culture);
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _db.Language;
        }
    }
}
