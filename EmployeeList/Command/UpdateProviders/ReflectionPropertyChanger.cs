using System.Globalization;
using System.Reflection;
using EmployeeList.Model;

namespace EmployeeList.Command.UpdateProviders;

/// <summary>
/// Класс для используемый для заполнения полей класса <see cref="Employee"/>
/// Основывается на рефлексии, получая доступ к публичным полям класса по их названию
/// <remarks>
/// Поддерживает множественное редактирование полей,
/// При указаннии одинаковых полей, поле повторно изменено не будет
/// </remarks>
/// <seealso cref="ConditionalEmployeePropertyChanger"/>
/// </summary>
public class ReflectionPropertyChanger : IEmployeePropertyChanger
{
    /// <summary>
    /// Производит заполнение полей <see cref="Employee"/> через доступ к полям класса рефлексией
    /// </summary>
    /// <param name="employee">Запись, к заполнению</param>
    /// <param name="changes">Набор изменений</param>
    /// <remarks> Поле Id не доступно для редактирования </remarks>
    /// <returns>Обновлена ли запись</returns>
    public bool Update(Employee employee, IEnumerable<IEmployeePropertyChanger.PropertyChanges> changes)
    {
        foreach (var (propertyName, propertyValue) in changes)
        {
            if (propertyName == nameof(Employee.Id))
            {
                Console.WriteLine("Error: Cannot update Id property.");
                return false;
            }

            var employeeType = typeof(Employee);

            // Получение данных по публичному полю класса
            var propertyInfo = employeeType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                Console.WriteLine($"Error: Property '{propertyName}' not found on Employee.");
                return false;
            }
            
            // Приведение нового значения поля к типу поля
            object value = Convert.ChangeType(propertyValue, propertyInfo.PropertyType, CultureInfo.InvariantCulture);
            // Установка значения в экзэмпляр employee
            propertyInfo.SetValue(employee, value);
        }
        
        Console.WriteLine($"Employee with Id: {employee.Id}, has been updated successfully.");
        return true;
    }
}