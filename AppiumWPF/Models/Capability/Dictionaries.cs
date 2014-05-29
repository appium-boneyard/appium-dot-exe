using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Appium.Models.Capability
{
    /// <summary>
    /// Contains a list of read only dictionaries
    /// </summary>
    internal static class Dictionaries
    {
        #region General 

        #region Languages
        /// <summary>
        /// List of supported languages
        /// </summary>
        private static readonly List<string> _LanguageList = new List<string>()
        {
            "ar",
            "bg",
            "ca",
            "cs",
            "da",
            "de",
            "el",
            "en",
            "es",
            "fi",
            "fr",
            "he",
            "hi",
            "hr",
            "hu",
            "id",
            "it",
            "iw",
            "ja",
            "ko",
            "li",
            "lt",
            "lv",
            "ms",
            "nb",
            "nl",
            "pl",
            "pt",
            "ro",
            "ru",
            "sk",
            "sl",
            "sr",
            "sv",
            "th",
            "tl",
            "tr",
            "uk",
            "vi",
            "zh_CN",
            "zh_TW"
        };

        /// <summary>
        /// List of supported languages
        /// </summary>
        public static ReadOnlyCollection<string> LanguageList = _LanguageList.AsReadOnly();
        #endregion Languages

        #region Locale List
        /// <summary>
        /// List of supported locales
        /// </summary>
        private static readonly List<string> _LocaleList = new List<string>()
        {
            "AT",
            "AU",
            "BE",
            "BG",
            "BR",
            "CA",
            "CH",
            "CN",
            "CZ",
            "DE",
            "DK",
            "EG",
            "ES",
            "FI",
            "FR",
            "GB",
            "GR",
            "HR",
            "HU",
            "ID",
            "IE",
            "IL",
            "IN",
            "JP",
            "KR",
            "LI",
            "LT",
            "LV",
            "NL",
            "NO",
            "NZ",
            "PH",
            "PL",
            "PT",
            "RO",
            "RS",
            "RU",
            "SE",
            "SG",
            "SK",
            "TH",
            "TR",
            "TW",
            "UA",
            "US",
            "VN",
            "ZA"
        };

        /// <summary>
        /// List of supported Locales
        /// </summary>
        public static ReadOnlyCollection<string> LocaleList = _LocaleList.AsReadOnly(); 
        #endregion Locale List

        #endregion General 

        #region Android Specific

        #region Platform Names
        /// <summary>
        /// List of supported Platform Names
        /// </summary>
        private static readonly List<string> _PlatformNameList = new List<string>()
        {
            "Android", 
            "FirefoxOS"
        };

        /// <summary>
        /// read only list of platform names
        /// </summary>
        public static ReadOnlyCollection<string> PlatformNameList = _PlatformNameList.AsReadOnly(); 
        #endregion Platform Names

        #region Platform Versions

        /// <summary>
        /// List of supported Platform Versions
        /// </summary>
        private static readonly Dictionary<string, string> _PlatformVersions = new Dictionary<string, string>()
        {
            {"4.4 KitKat (API Level 19)", "19"},
            {"4.3 Jelly Bean (API Level 18)", "18"},
            {"4.2 Jelly Bean (API Level 17)", "17"},
            {"4.1 Jelly Bean (API Level 16)", "16"},
            {"4.0.3 Ice Cream Sandwich (API Level 15)", "15"},
            {"4.0 Ice Cream Sandwich (API Level 14)", "14"},
            {"3.2 Honeycomb (API Level 13)", "13"},
            {"3.1 Honeycomb (API Level 12)", "12"},
            {"3.0 Honeycomb (API Level 11)", "11"},
            {"2.3.3 Gingerbread (API Level 10)", "10"},
            {"2.3 Gingerbread (API Level 9)", "9"},
            {"2.2 Froyo (API Level 8)", "8"},
            {"2.1 Eclair (API Level 7)", "7"},
            {"2.0.1 Eclair (API Level 6)", "6"},
            {"2.0 Eclair (API Level 5)", "5"},
            {"1.6 Donut (API Level 4)", "4"},
            {"1.5 Cupcake (API Level 3)", "3"},
            {"1.1 (API Level 2)", "2"},
            {"1.0 (API Level 1)", "1"}
        };

        /// <summary>
        /// Get the Platform Version number
        /// </summary>
        /// <param name="version">string representation of the version</param>
        /// <returns>version number</returns>
        public static string GetPlatformVersion(string version)
        {
            return (_PlatformVersions.ContainsKey(version)) ? _PlatformVersions[version] : null;
        }

        /// <summary>
        /// List of supported Platform Versions
        /// </summary>
        public static ReadOnlyCollection<string> PlatformVersionList = _PlatformVersions.Keys.ToList().AsReadOnly();
        #endregion Platform Versions

        #region Automation Names
        /// <summary>
        /// List of supported Automation Names
        /// </summary>
        private static readonly List<string> _AutomationNameList = new List<string>()
        {
            "Appium",
            "Selendroid"
        };

        /// <summary>
        /// List of supported Automation Names
        /// </summary>
        public static ReadOnlyCollection<string> AutomationNameList = _AutomationNameList.AsReadOnly();
        #endregion Automation Names

        #endregion Android Specific
    }
}
