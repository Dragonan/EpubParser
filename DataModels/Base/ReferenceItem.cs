using EpubParser.DataModels.Interfaces;
using Newtonsoft.Json;
using EpubParser.Helpers;

namespace EpubParser.DataModels.Base
{
    public abstract class ReferenceItem<T>: IReferenceItem<T> where T : IEntity
    {
        private int _id;
        public int ID
        { 
            get { return _id; }
            set
            {
                _id = value;

                if (Entity == null || Entity.ID != _id)
                    Entity = DataCache.Get<T>(_id);
            }
        }

        [JsonIgnore]
        public T Entity { get; set; }

        public ReferenceItem() { }

        public ReferenceItem(int id, T entity = default(T))
        {
            Entity = entity;
            ID = id;
        }
    }
}