using System;
using System.Collections.Generic;
using System.IO;
using EpubParser.DataModels;
using Newtonsoft.Json;

namespace EpubParser.Helpers
{
    public static class DataCache
    {
        public static EntityList<Model> Models { get; set; }
        public static EntityList<Wargear> Wargear { get; set; }
        public static EntityList<WargearList> WargearLists { get; set; }

        public static void Init()
        {
            Models = new EntityList<Model>();
            Wargear = new EntityList<Wargear>();
            WargearLists = new EntityList<WargearList>();
        }

        public static void Load()
        {
            Models.Load(FilePaths.ModelsJson);
            Wargear.Load(FilePaths.WargearJson);
            WargearLists.Load(FilePaths.WargearListsJson);
        }

        public static void Save()
        {
            Models.Save(FilePaths.ModelsJson);
            Wargear.Save(FilePaths.WargearJson);
            WargearLists.Save(FilePaths.WargearListsJson);
            
        }
    }
}