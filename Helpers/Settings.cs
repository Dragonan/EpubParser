using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EpubParser.Helpers
{
    public static class Settings
    {
        public static IConfigurationRoot AppConfig { get; private set; }
        public static IConfigurationRoot BookConfig { get; private set; }

        static Settings()
        {
            AppConfig = OpenConfig("appsettings.json", Directory.GetCurrentDirectory());
        }

        public static void SetBookConfig(string file, string dir)
        {
            BookConfig = OpenConfig(file, dir);
        }

        private static IConfigurationRoot OpenConfig(string file, string dir)
        {
            return new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile(file).Build();
        }
    }
}