using EmployeeList.AppConstants;
using EmployeeList.Command.UpdateProviders;
using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Обновляет данные о сотруднике через провайдеры изменения полей <see cref="IEmployeePropertyChanger"/>
/// <example> Примеры:
/// <see cref="ReflectionPropertyChanger"/>
/// <see cref="ConditionalEmployeePropertyChanger"/>
/// </example>
/// </summary>
public class UpdateCommand : BaseCommand
{
    public int Id { get; init; }

    public string[] ChangesList = { };

    public IEmployeePropertyChanger PropertyChanger { get; set; } = new ReflectionPropertyChanger();

    /// <summary>
    /// Обновляет данные о сотруднике по переданным данным
    /// </summary>
    public override bool Execute()
    {
        // Конвертирование в формат {Property,Value} для дальней обработки
        var changesConverted = ChangesList
            .Select(x => x.Split(AppConstant.Input.ARGS_SEPARATOR))
            .Select(y => new IEmployeePropertyChanger.PropertyChanges(y[0], y[1]));
        
        var employee = _repository.Get(Id);
        if (employee is null)
        {
            Console.WriteLine($"Error: Employee with Id {Id} not found.");
            return false;
        }
        
        // Сохранение исходного состояния сотрудника
        var originalEmployee = new Employee
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            SalaryPerHour = employee.SalaryPerHour
        };
        
        var updateSuccess = PropertyChanger.Update(employee, changesConverted);
        
        // Восстановление исходного состояния сотрудника при ошибке
        if (false == updateSuccess)
        {
            _repository.Update(employee.Id, originalEmployee);
            return false;
        }

        return true;
    }
    

    public UpdateCommand(IRepository<Employee> repository) : base(repository) { }
}