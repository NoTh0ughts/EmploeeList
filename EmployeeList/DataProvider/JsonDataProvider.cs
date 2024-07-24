using EmployeeList.Model;

namespace EmployeeList.DataProvider;

public class JsonDataProvider : IRepository<Employee>, IDisposable
{
    public Dictionary<int, Employee> Employees = new();

    private readonly string _filePath;
    
    public JsonDataProvider(string filePath)
    {
        _filePath = filePath;
        // TODO load here
    }

    public IEnumerable<Employee> GetAll() => Employees.Values;

    public Employee? Get(int id) => Employees.FirstOrDefault(x => x.Value.Id == id).Value;

    public Employee Update(int id, Employee item) => Employees[id] = item;

    public bool Delete(int id) => Employees.Remove(id);

    public void Add(Employee item)
    {
        int newId = Employees.Keys.Max() + 1;
        Employees.Add(newId, item);
    }

    public void Dispose()
    {
        // TODO make a save theres
    }
}