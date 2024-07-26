using System.Text.Json;
using EmployeeList.DataProvider.FileSource;
using EmployeeList.Model;

namespace EmployeeList.DataProvider;

/// <summary>
/// Источник данных на основе JSON - файлов
/// </summary>
public class JsonDataProvider : IRepository<Employee>
{
    private readonly string _filePath;
    private readonly IDataSource _dataSource;

    /// <summary>
    /// Словарь работников в формате Id, Employee
    /// </summary>
    public Dictionary<int, Employee> Employees = new();

    public JsonDataProvider(string filePath, IDataSource dataSource)
    {
        _filePath = filePath;
        _dataSource = dataSource;
        
        var jsonString = dataSource.GetRawData(_filePath);
        
        var employeesList =  JsonSerializer.Deserialize<IEnumerable<Employee>>(jsonString);
        if (employeesList != null)
        {
            Employees = employeesList.ToDictionary(e => e.Id);
        }
    }

    /// <summary>
    /// Получить всех работников
    /// </summary>
    /// <param name="chunkSize">Ограничение чанка записей, по умолчанию - 50</param>
    /// <returns>Список всех работников</returns>
    public IEnumerable<Employee> GetAll(int chunkSize = 50) => 
        Employees.Select(x => x.Value)
                 .Take(chunkSize);

    /// <summary>
    /// Находит работника с указанным Id
    /// </summary>
    /// <param name="id">Уникальный идентификатор</param>
    /// <returns>Запись найденного сотрудника, иначе null</returns>
    public Employee? Get(int id)
    {
        var employeeEntry = Employees.FirstOrDefault(x => x.Value.Id == id);
        return employeeEntry.Equals(default(KeyValuePair<int, Employee>)) ? null : employeeEntry.Value;
    }
    
    /// <summary>
    /// Обновляет запись работника
    /// </summary>
    /// <param name="id">Уникальный идентификатор</param>
    /// <param name="item">Заполненная запись сущ. работника</param>
    /// <returns>Удалось ли обновить запись</returns>
    public bool Update(int id, Employee item)
    {
        if (false == Employees.ContainsKey(id)) return false;
        
        Employees[id] = item;
        return true;
    }

    /// <summary>
    /// Удаляет сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Уникальный идентификатор</param>
    /// <returns>Удалось ли удалить</returns>
    public bool Delete(int id) => Employees.Remove(id);

    /// <summary>
    /// Добавляет нового сотрудника,
    /// Его идентификатор определяется как (максимальный имеющийся идентификатор + 1)
    /// </summary>
    /// <param name="item">Сотрудник к добавлению</param>
    /// <returns>Добавленная запись</returns>
    public Employee Add(Employee item)
    {
        int newId = item.Id =  Employees.Any() ? Employees.Keys.Max() + 1 : 1;
        Employees.Add(newId, item);

        return Employees[newId];
    }

    /// <summary>
    /// Производит сохранение данных
    /// </summary>
    public void Commit()
    {
        var dataToWrite = JsonSerializer.Serialize<IEnumerable<Employee>>(Employees.Values);
        _dataSource.Save(_filePath, dataToWrite);
    }

    /// <summary>
    /// При деструктуризации сохраняем изменения
    /// </summary>
    public void Dispose()
    {
        Commit();
    }
}