using Hotel.Atr3.RealPortal.Models;

namespace Hotel.Atr3.RealPortal.Service
{
    public interface ILocalizationService
    {
        public StringResources GetStringResources(string recourceKey, 
            int languageId);
    }
}
