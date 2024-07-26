using EmployeeList.Model;

namespace EmployeeList.Command.UpdateProviders;

/// <summary>
/// Интерфейс для редактирования записей работников
/// Допуская возможность неполного описания работника (для случая отсуствия фамилии, почасовой ставки)
/// <example> Примеры реализации:
/// <see cref="ReflectionPropertyChanger"/>
/// <see cref="ConditionalEmployeePropertyChanger"/>
/// </example>
/// </summary>
public interface IEmployeePropertyChanger
{
    /// <summary>
    /// Производит обновление полей класса согласно переданным изменениям
    /// </summary>
    /// <param name="employee">Запись для редактирования</param>
    /// <param name="propertyChanges">Набор данных записи к изменения</param>
    /// <returns>Выполнено ли обновление записи</returns>
    public bool Update(Employee employee, IEnumerable<PropertyChanges> propertyChanges);
    
    public record PropertyChanges(string Property, string Value);
}