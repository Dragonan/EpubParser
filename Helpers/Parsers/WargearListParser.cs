using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EpubParser.DataModels;
using HtmlAgilityPack;

namespace EpubParser.Helpers.Parsers
{
    public static class WargearListParser
    {
        public static void ParsePage(string file)
        {
            using (var fs = File.OpenRead(file))
            {
                var doc = new HtmlDocument();
                doc.Load(fs);

                var segments = doc.DocumentNode.SelectNodes("//*[@class='Basic-Text-Frame']");

                foreach (var segment in segments)
                {
                    ParseSegment(segment);
                }

                fs.Close();
            }
        }

        private static void ParseSegment(HtmlNode node)
        {
            var selector = "//*[@id='" + node.Attributes["id"].Value + "']//*[@class='CharOverride-15']/../..";
            node = node.SelectSingleNode(selector);
        
            if (node == null)
                return;

            var list = new WargearList();
            var limit = 0;

            for (var i=0; i<node.ChildNodes.Count; i++)
            {
                var row = node.ChildNodes[i];
                var rowText = GetText(row);
                if (String.IsNullOrEmpty(rowText))
                    continue;

                if (row.ChildNodes[0].HasClass("CharOverride-14"))
                {
                    var name = rowText.ToUpper();
                    list = DataCache.WargearLists.Get(name) ?? DataCache.WargearLists.Create(new WargearList(name));
                    limit = 0;
                    continue;
                }

                if (row.ChildNodes[0].HasClass("CharOverride-10"))
                {
                    if (rowText.StartsWith("Up to two weapons can be"))
                    {
                        limit = 2;
                        continue;
                    }

                    if (rowText.StartsWith("One weapon can be"))
                    {
                        limit = 1;
                        continue;
                    }

                    throw new Exception("Unknown text encountered while parsing wargear lists: \"" + rowText + "\"");
                }

                if (row.ChildNodes[0].HasClass("CharOverride-15"))
                {
                    var name = rowText.TrimStart(new []{'•', ' '});
                    var alias = Settings.BookConfig["aliases:" + name];
                    name = alias ?? name;

                    var wargear = DataCache.Wargear.Get(name);
                    if (wargear == null)
                        throw new Exception("Wargear List contains items not found in database: " + rowText + "\"");

                    list.Items.Add(new WargearListOption(wargear.ID, limit));
                }
            }
        }

        private static string GetText(HtmlNode node)
        {
            return String.Join(" ", node.ChildNodes
                    .Where(x => x.NodeType == HtmlNodeType.Element)
                    .Select(x => x.InnerText.Trim()))
                    .Replace("&#160;", " ");
        }
    }
}