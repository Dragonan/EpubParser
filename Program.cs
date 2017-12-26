using System;
using EpubParser.Helpers;
using EpubParser.Helpers.Parsers;

namespace EpubParser
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Setup(args);
                DataCache.Load();

                //PointParser.ParsePages(FilePaths.PointPages);
                WargearListParser.ParsePage(FilePaths.WargearListPage);

                DataCache.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Setup(string[] args)
        {
                if (args.Length == 0 || String.IsNullOrWhiteSpace(args[0]))
                    throw new Exception("No book selected");

                Settings.Init();
                Constants.Init();
                FilePaths.Init(args[0]);
                DataCache.Init();
        }
    }
}