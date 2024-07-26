using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Добавляет нового сотрудника,
/// Выполняет добавление пустого сотрудника, после чего выполняет заполнение через <see cref="UpdateCommand"/>
/// Такой подход позволяет указывать не все поля класса, при его создании
/// </summary>
public class AddCommand : BaseCommand
{
    public string[] Changes { get; init; } = { };

    /// <summary>
    /// Добавляет нового сотрудника
    /// <returns>Выполнена ли команда</returns>
    /// </summary>
    public override bool Execute()
    {
        if (Changes.Length == 0)
        {
            Console.WriteLine("Cannot add new employee - not enough data");
            return false;
        }
        
        var newEmployee = _repository.Add(new Employee());
        var command     = new UpdateCommand(_repository)
        {
            Id          = newEmployee.Id,
            ChangesList = Changes,
        };
        
        return command.Execute();
    }

    public AddCommand(IRepository<Employee> repository) : base(repository) { }
}