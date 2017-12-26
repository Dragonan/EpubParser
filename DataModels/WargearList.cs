using System;
using System.Collections.Generic;
using EpubParser.DataModels.Interfaces;

namespace EpubParser.DataModels
{
    public class WargearList: IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<WargearListOption> Items { get; set; }

        public WargearList() : this(null) { }

        public WargearList(string name)
        {
            Name = name;
            Items = new List<WargearListOption>();
        }
    }

    public class WargearListOption
    {
        public int ID { get; set; }
        public int Limit { get; set; }

        public WargearListOption() { }

        public WargearListOption(int id, int limit)
        {
            ID = id;
            Limit = limit;
        }
    }
}