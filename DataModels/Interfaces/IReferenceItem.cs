namespace EpubParser.DataModels.Interfaces
{
    public interface IReferenceItem<T> where T: IEntity
    {
        int ID { get; set; }
        T Entity { get; set; }
    }
}