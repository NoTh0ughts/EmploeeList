using EmployeeList.Model;

namespace EmployeeList.Command.UpdateProviders;

/// <summary>
/// Класс для используемый для заполнения полей класса <see cref="Employee"/>
/// Основывается на наборе условий по которым новые значения устанавливаются в поле
/// <remarks>
/// Поддерживает множественное редактирование полей,
/// При указаннии одинаковых полей, поле повторно изменено не будет
/// </remarks>
/// 
/// <seealso cref="ReflectionPropertyChanger"/>
/// </summary>
public class ConditionalEmployeePropertyChanger : IEmployeePropertyChanger
{
    /// <summary>
    /// Производит заполнение полей <see cref="Employee"/> по набору сравнений с именами полей класса
    /// </summary>
    /// <param name="employee">Запись, к заполнению</param>
    /// <param name="changes">Набор изменений</param>
    /// <remarks> Поле Id не доступно для редактирования </remarks>
    /// <returns>Обновлена ли запись</returns>
    public bool Update(Employee employee, IEnumerable<IEmployeePropertyChanger.PropertyChanges> changes)
    {
        var updatedFields = new List<string>();
        
        foreach (var (property, value) in changes)
        {
            if (property == nameof(Employee.Id))
            {
                Console.WriteLine("Error: Cannot update Id property.");
                return false;
            }

            if (updatedFields.Contains(property))
            {
                Console.WriteLine($"Property {property} already assigned, value is not changed.");
                continue;
            }

            switch (property)
            {
                case nameof(Employee.FirstName):
                    employee.FirstName = value;
                    break;
                case nameof(Employee.LastName):
                    employee.LastName = value;
                    break;
                case nameof(Employee.SalaryPerHour):
                    employee.SalaryPerHour = Convert.ToDecimal(value);
                    break;
                default:
                    Console.WriteLine($"Property name: {property} invalid, make sure that input is correct");
                    return false;
            }
            updatedFields.Add(property);
        }
        
        Console.WriteLine($"Employee with Id: {employee.Id}, has been updated successfully.");
        return true;
    }
}