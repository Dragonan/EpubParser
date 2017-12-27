using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EpubParser.DataModels;
using HtmlAgilityPack;

namespace EpubParser.Helpers.Parsers
{
    public static class PointParser
    {
        public static void ParsePages(IEnumerable<string> fileNames)
        {
            foreach (var page in fileNames)
            {
                ParsePage(page);
            }
        }

        private static void ParsePage(string file)
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
            var selector = "//*[@id='" + node.Attributes["id"].Value + "']//*[@class='CharOverride-5']/../..";
            var nodes = node.SelectNodes(selector);
        
            if (nodes == null)
                return;

            var startIndex = nodes[0].InnerText.Trim().StartsWith("POINTS") ? 1 : 0;
            var isModel = nodes.Count > (startIndex+2) && Regex.IsMatch(GetText(nodes[startIndex+2]), @"^\d+/?\d*$");
            var increment = isModel ? 3 : 2;
            var mainName = "";

            for (var i=startIndex; i<nodes.Count; i+=increment)
            {
                var rawName = FixName(nodes[i]);
                var rawCost = GetText(nodes[i+ (isModel ? 2 : 1)]);

                if (isModel)
                {
                    var cost = Int32.Parse(rawCost);
                    var isSubModel = rawName.StartsWith('-');
                    if (isSubModel)
                        rawName = mainName + Constants.ModelNameSeparator + rawName.Substring(2);
                    else
                        mainName = rawName;

                    DataCache.Models.Create(new Model(rawName, cost)); 
                }
                else
                {
                    var name = Regex.Replace(rawName, @"\(([^\)]+)\)", "").Trim();
                    var costs = rawCost.Split('/');
                    var cost = Int32.Parse(costs[0]);
                    var cost2 = costs.Length > 1 ? (int?)Int32.Parse(costs[1]) : null;
                    var tags = new string[] {};

                    var wargear = DataCache.Wargear.Get(name) ?? DataCache.Wargear.Create(new Wargear(name));
                    var clarifications = Regex.Matches(rawName, @"\(([^\)]+)\)");
                    foreach (Match clarification in clarifications)
                    {
                        var clr = clarification.Groups[1].Value;

                        if (clr == "single/pair")
                            continue;

                        if (clr == "other models")
                            break;
                        
                        if (clr.Contains(',') || clr.Contains(" and "))
                        {
                            tags = clr.Replace(" and ", ", ").Split(", ").Select(x => x.ToUpper()).ToArray();
                            break;
                        }

                        var tag = clr.ToUpper();
                        var alias = Settings.BookConfig["aliases:" + tag];
                        tags = new [] { alias ?? tag };
                    }

                    if (tags.Any())
                    {
                        var specialCost = wargear.SpecialCosts.FirstOrDefault(x => x.Tags.SequenceEqual(tags));
                        if (specialCost != null)
                        {
                            specialCost.Cost = cost;
                            specialCost.SecondCost = cost2;
                        }
                        else
                        {
                            wargear.SpecialCosts.Add(new SpecialCost(tags, cost, cost2));
                        }
                    }
                    else
                    {
                        wargear.Cost = cost;
                        wargear.SecondCost = cost2;
                    }
                }
            }
        }

        private static string FixName(HtmlNode node)
        {
            var name = String.Join(" ", node.ChildNodes
                    .Where(x => x.NodeType == HtmlNodeType.Element)
                    .Select(x => GetText(x)))
                    .Replace("&#160;", " ");
            
            var alias = Settings.BookConfig["aliases:" + name];

            return alias ?? name;
        }

        private static string GetText(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}