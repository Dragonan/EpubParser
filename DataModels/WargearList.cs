using System;
using System.Collections.Generic;
using EpubParser.DataModels.Base;
using EpubParser.DataModels.Interfaces;
using EpubParser.Helpers;
using Newtonsoft.Json;

namespace EpubParser.DataModels
{
    public class WargearList: IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ReferenceList<WargearListOption, Wargear> Items { get; set; }

        public WargearList() : this(null) { }

        public WargearList(string name)
        {
            Name = name;
            Items = new ReferenceList<WargearListOption, Wargear>();
        }
    }

    public class WargearListOption: ReferenceItem<Wargear>
    {
        public int Limit { get; set; }

        public WargearListOption() { }

        public WargearListOption(int id, int limit, Wargear entity = null)
            :base(id, entity)
        {
            Limit = limit;
        }
    }
}