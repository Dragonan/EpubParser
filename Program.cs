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
                if (args.Length == 0 || String.IsNullOrWhiteSpace(args[0]))
                    throw new Exception("No book selected");

                Book.Load(args[0]);
                DataCache.Load();

                //PointParser.ParsePages(Book.PointPages);
                //WargearListParser.ParsePage(Book.WargearListPage);

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
    }
}