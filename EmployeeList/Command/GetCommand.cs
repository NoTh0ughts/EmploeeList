using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Производит поиск сотрудника по его идентификатору
/// </summary>
public class GetCommand : BaseCommand
{
    public int Id { get; init; }
    
    /// <summary>
    /// Производит поиск и вывод на экран сотрудника по его идентификатору
    /// </summary>
    public override bool Execute()
    {
        var employee = _repository.Get(Id);
        if (employee is null)
        {
            Console.WriteLine($"Cannot find item with Id = {Id}");
            return false;
        }
        
        Console.WriteLine(employee);
        return true;
    }

    public GetCommand(IRepository<Employee> repository) : base(repository) { }
}