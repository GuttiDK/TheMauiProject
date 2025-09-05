using Microsoft.Maui.Storage;

namespace MauiHello.Services
{
    public static class SettingsService
    {
        private const string DefaultTipPercentKey = "DefaultTipPercent";
        private const double DefaultTipPercentValue = 15.0;

        public static double GetDefaultTipPercent()
        {
            return Preferences.Get(DefaultTipPercentKey, DefaultTipPercentValue);
        }

        public static void SetDefaultTipPercent(double tipPercent)
        {
            Preferences.Set(DefaultTipPercentKey, tipPercent);
        }

        public static void ResetToDefaultTipPercent()
        {
            Preferences.Set(DefaultTipPercentKey, DefaultTipPercentValue);
        }
    }
}