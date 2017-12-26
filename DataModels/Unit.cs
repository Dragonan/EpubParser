using System;
using System.Collections.Generic;
using EpubParser.DataModels.Interfaces;

namespace EpubParser.DataModels
{
    public class Unit: IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public List<string> Tags { get; set; }
        public List<string> Factions { get; set; }

        public Unit() :this(null) { }

        public Unit(string name)
        {
            Name = name;
            Tags = new List<string>();
            Factions = new List<string>();
        }
    }
}