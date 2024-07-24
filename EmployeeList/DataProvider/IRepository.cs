namespace EmployeeList.DataProvider;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();
    public T Get   (int id);
    public T Update(int id, T item);
    public T Delete(int id);
    public T Add   (T item);
}