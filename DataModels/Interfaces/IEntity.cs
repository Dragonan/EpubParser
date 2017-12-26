using System;

namespace EpubParser.DataModels.Interfaces
{
    public interface IEntity
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}