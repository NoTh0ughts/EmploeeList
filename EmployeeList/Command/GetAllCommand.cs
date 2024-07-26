using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Выводит на экран всех сотрудников
/// </summary>
public class GetAllCommand : BaseCommand
{
    /// <summary>
    /// Выводит на экран всех сотрудников.
    /// Определение представления в строковом виде находится в <see cref="Employee"/>
    /// </summary>
    public override bool Execute()
    {
        foreach (var employee in _repository.GetAll())
        {
            Console.WriteLine(employee.ToString());
        }

        return true;
    }

    public GetAllCommand(IRepository<Employee> repository) : base(repository) { }
}