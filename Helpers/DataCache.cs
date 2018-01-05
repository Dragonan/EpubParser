using System;
using System.Collections.Generic;
using System.IO;
using EpubParser.DataModels;
using EpubParser.DataModels.Base;
using EpubParser.DataModels.Interfaces;
using Newtonsoft.Json;

namespace EpubParser.Helpers
{
    public static class DataCache
    {
        public static EntityList<Model> Models { get; set; }
        public static EntityList<Wargear> Wargear { get; set; }
        public static EntityList<WargearList> WargearLists { get; set; }

        static DataCache()
        {
            Models = new EntityList<Model>();
            Wargear = new EntityList<Wargear>();
            WargearLists = new EntityList<WargearList>();
        }

        public static T Get<T>(int id) where T: IEntity
        {
            if (typeof(T) == typeof(Model))
                return (T)(object)Models.Get(id);
            if (typeof(T) == typeof(Wargear))
                return (T)(object)Wargear.Get(id);
            if (typeof(T) == typeof(WargearList))
                return (T)(object)WargearLists.Get(id);

            return default(T);
        }

        public static void Load()
        {
            Models.Load(Book.ModelsJson);
            Wargear.Load(Book.WargearJson);
            WargearLists.Load(Book.WargearListsJson);
        }

        public static void Save()
        {
            Models.Save(Book.ModelsJson);
            Wargear.Save(Book.WargearJson);
            WargearLists.Save(Book.WargearListsJson);
            
        }
    }
}