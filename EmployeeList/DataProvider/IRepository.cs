namespace EmployeeList.DataProvider;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();
    public T? Get   (int id);
    public T? Update(int id, T item);
    public bool Delete(int id);
    public void Add(T item);
}