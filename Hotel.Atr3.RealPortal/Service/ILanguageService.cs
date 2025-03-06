using Hotel.Atr3.RealPortal.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Hotel.Atr3.RealPortal.Service
{
    public interface ILanguageService
    {
        public IEnumerable<Language> GetLanguages();
        public Language GetLanguageByCulture(string culture);
    }
}
