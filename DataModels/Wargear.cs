using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using EpubParser.DataModels.Interfaces;

namespace EpubParser.DataModels
{
    public class Wargear: IPointValueItem, IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public int? SecondCost { get; set; }
        public List<SpecialCost> SpecialCosts { get; set; }

        public Wargear() :this(null) { }

        public Wargear(string name, int cost = 0, int? secondCost = null)
        {
            Name = name;
            Cost = cost;
            SecondCost = secondCost;
            SpecialCosts = new List<SpecialCost>();
        }
    }
}