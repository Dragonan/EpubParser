using System.Collections.Generic;
using System.Linq;
using EpubParser.DataModels.Interfaces;

namespace EpubParser.DataModels.Base
{
    public class ReferenceList<R, E>: List<R> 
        where R: IReferenceItem<E> 
        where E: IEntity
    {
        public R Get(int id)
        {
            if (id <= 0)
                return default(R);

            return this.FirstOrDefault(x => x.ID == id);
            
        }

        public R Create(R item)
        {
            this.Add(item);
            return item;
        }
    }
}