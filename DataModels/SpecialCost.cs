using System;
using EpubParser.DataModels.Interfaces;

namespace EpubParser.DataModels
{
    public class SpecialCost: IPointValueItem
    {
        public string[] Tags { get; set; }
        public int Cost { get; set; }
        public int? SecondCost { get; set; }

        public SpecialCost() :this(new string[] {}, 0) { }

        public SpecialCost(string[] tags, int cost, int? secondCost = 0)
        {
            Tags = tags;
            Cost = cost;
            SecondCost = secondCost;
        }
    }
}