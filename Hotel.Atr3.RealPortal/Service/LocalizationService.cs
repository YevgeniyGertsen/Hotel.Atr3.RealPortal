using Hotel.Atr3.RealPortal.Models;

namespace Hotel.Atr3.RealPortal.Service
{
    public class LocalizationService : ILocalizationService
    {
        private readonly AppDbContext _db;
        public LocalizationService(AppDbContext db)
        {
            _db = db;
        }
        public StringResources GetStringResources(string recourceKey,
            int languageId)
        {
            return _db.StringResources
                .FirstOrDefault(f => f.Name == recourceKey
                && f.LanguageId == languageId);
        }
    }
}
