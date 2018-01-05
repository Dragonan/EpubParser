using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace EpubParser.Helpers
{
    public static class Book
    {
        public static string[] PointPages { get; private set; }
        public static string WargearListPage { get; private set; }

        public static string ModelsJson { get; private set; }
        public static string WargearJson { get; private set; }
        public static string WargearListsJson { get; private set; }

        public static void Load(string book)
        {
            var appDir = Directory.GetCurrentDirectory() + @"\";
            var epubsDir = appDir + Settings.AppConfig["filePaths:epubPath"];
            var bookDir = Settings.AppConfig["books:" + book.ToUpper()];
            if (String.IsNullOrWhiteSpace(bookDir))
                throw new Exception("Invalid book");

            bookDir = epubsDir + bookDir;

            if (!Directory.Exists(bookDir))
                throw new Exception("Book not found");

            Settings.SetBookConfig("bookinfo.json", bookDir);
            
            var pagesDir = bookDir + Settings.AppConfig["filePaths:pagesPath"];
            PointPages = Settings.BookConfig.GetSection("pointValues").GetChildren().Select(x => pagesDir + x.Value).ToArray();
            WargearListPage = pagesDir + Settings.BookConfig["wargearLists"];
            
            var jsonsDir = appDir + Settings.AppConfig["filePaths:jsonsPath"] + book.ToUpper() + @"\";
            if (!Directory.Exists(jsonsDir))
                Directory.CreateDirectory(jsonsDir);

            ModelsJson = jsonsDir + Settings.AppConfig["jsonFileNames:models"];
            WargearJson = jsonsDir + Settings.AppConfig["jsonFileNames:wargear"];
            WargearListsJson = jsonsDir + Settings.AppConfig["jsonFileNames:wargearLists"];
        }
    }
}