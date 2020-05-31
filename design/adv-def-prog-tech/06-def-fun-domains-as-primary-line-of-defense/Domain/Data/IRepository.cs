namespace def_fun_domains_as_primary_line_of_defense
{
    public interface IRepository<T>
    {
        T Find(int id);
        void Add(T obj);
        void Delete(T obj);
        void SaveChanges();
    }
}
