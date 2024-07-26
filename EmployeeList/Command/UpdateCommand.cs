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

    private readonly IEmployeePropertyChanger _propertyChanger = new ReflectionPropertyChanger();
    
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
        return _propertyChanger.Update(employee, changesConverted);
    }

    public UpdateCommand(IRepository<Employee> repository) : base(repository) { }
}