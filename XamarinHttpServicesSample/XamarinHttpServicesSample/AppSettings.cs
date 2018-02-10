using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinHttpServicesSample
{
    public static class AppSettings
    {
        //Endpoints
        private const string DefaultMonkeysEndpoint = "YOUR_HOTELS_ENDPOINT";

        private static ISettings Settings => CrossSettings.Current;

        public static string MonkeysEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(MonkeysEndpoint), DefaultMonkeysEndpoint);

            set => Settings.AddOrUpdateValue(nameof(MonkeysEndpoint), value);
        }
    }
}
