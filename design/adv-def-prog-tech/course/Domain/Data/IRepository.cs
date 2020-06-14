namespace Course
{
    public interface IRepository<T>
    {
        T Find(int id);
        void Add(T obj);
        void Delete(T obj);
        void SaveChanges();
    }
}
