namespace Scheduler.Application.Interfaces
{
    public interface IService<T>
    {
        bool Update(T item);

        bool Create(T item);

        List<T> Get();

        public bool Delete(Guid id);
    }
}