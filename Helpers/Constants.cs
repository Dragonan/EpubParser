using System;

namespace EpubParser.Helpers
{
    public static class Constants
    {
        public static string ModelNameSeparator { get; private set; }

        public static void Init()
        {
            ModelNameSeparator = Settings.AppConfig["nameSeparator"];
        }
    }
}