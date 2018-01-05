using System;

namespace EpubParser.Helpers
{
    public static class Constants
    {
        public static string ModelNameSeparator { get; private set; }

        static Constants()
        {
            ModelNameSeparator = Settings.AppConfig["nameSeparator"];
        }
    }
}