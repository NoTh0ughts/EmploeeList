namespace EmployeeList.DataProvider;

/// <summary>
/// Интерфейс взаимодействия с источником данных о сотрудниках
/// Содержит основные CRUD операции
/// </summary>
/// <typeparam name="T">Тип оперируемых данных</typeparam>
public interface IRepository<T> : IDisposable
{
    public IEnumerable<T> GetAll(int chunkSize = 50);
    public T? Get   (int id);
    public bool Update(int id, T item);
    public bool Delete(int id);
    public T Add(T item);
    public void Commit();
}