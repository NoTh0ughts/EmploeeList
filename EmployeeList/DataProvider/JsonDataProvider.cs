using EmployeeList.Model;

namespace EmployeeList.DataProvider;

public class JsonDataProvider : IRepository<Employee>, IDisposable
{
    public List<Employee> Employees = new();

    private readonly string _filePath;
    
    public JsonDataProvider(string filePath)
    {
        _filePath = filePath;
        // TODO load here
    }

    public IEnumerable<Employee> GetAll()
    {
        throw new NotImplementedException();
    }

    public Employee Get(int id)
    {
        throw new NotImplementedException();
    }

    public Employee Update(int id, Employee item)
    {
        throw new NotImplementedException();
    }

    public Employee Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Employee Add(Employee item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
}