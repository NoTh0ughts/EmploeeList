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
        var updatedFields = new Dictionary<string, bool>();
        
        foreach (var propertyChanges in changes)
        {
            if (propertyChanges.Property == nameof(Employee.Id))
            {
                Console.WriteLine("Error: Cannot update Id property.");
                return false;
            }

            if (updatedFields.ContainsKey(propertyChanges.Property))
            {
                Console.WriteLine($"Property {propertyChanges.Property} already assigned, value is not changed.");
                continue;
            }

            switch (propertyChanges.Property)
            {
                case nameof(Employee.FirstName):
                    employee.FirstName = propertyChanges.Value;
                    break;
                case nameof(Employee.LastName):
                    employee.LastName = propertyChanges.Value;
                    break;
                case nameof(Employee.SalaryPerHour):
                    employee.SalaryPerHour = Convert.ToDecimal(propertyChanges.Value);
                    break;
                default:
                    Console.WriteLine($"Property name: {propertyChanges.Property} invalid, make sure that input is correct");
                    return false;
            }
        }
        
        Console.WriteLine($"Employee with Id: {employee.Id}, has been updated successfully.");
        return true;
    }
}