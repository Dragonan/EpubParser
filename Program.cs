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

                //PointParser.ParsePages(FilePaths.PointPages);
                //WargearListParser.ParsePage(FilePaths.WargearListPage);

                //DataCache.Save();

                foreach (var list in DataCache.WargearLists)
                {
                    Console.WriteLine(list.Name);
                    foreach (var item in list.Items)
                    {
                        Console.WriteLine(item.Entity.Name);
                    }
                }
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
                DataCache.Load();
        }
    }
}