using System;
using System.Collections.Generic;
using EpubParser.DataModels.Interfaces;
using Newtonsoft.Json;

namespace EpubParser.DataModels
{
    public class Model: IPointValueItem, IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public Model() { }

        public Model(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}