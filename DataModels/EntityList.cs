using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using EpubParser.DataModels.Interfaces;
using Newtonsoft.Json;

namespace EpubParser.DataModels
{
    public class EntityList<T>: List<T> where T: IEntity
    {
        public T Get(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return default(T);

            return this.FirstOrDefault(x => x.Name == name);
            
        }

        public T Create(T item)
        {
            item.ID = this.Count + 1;
            this.Add(item);
            return item;
        }

        public void Load(string file)
        {
            if (!File.Exists(file))
                return;
                
            var data = JsonConvert.DeserializeObject<T[]>(File.ReadAllText(file));
            this.Clear();
            this.AddRange(data);
        }

        public void Save(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}