using EmployeeList.Command.UpdateProviders;
using EmployeeList.Model;

namespace EmployeeTests;

public class EmployeePropertyChangerTests
{
    /// <summary>
    /// Тест, обработчик полей должен возвращать true, если сотрудник обновлен успешно
    /// </summary>
    [Theory]
    [MemberData(nameof(GetPropertyChangers))]
    public void Update_ShouldReturnTrue_WhenEmployeeUpdatedSuccessfully(IEmployeePropertyChanger propertyChanger)
    {
        var employee = new Employee { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        var changes = new List<IEmployeePropertyChanger.PropertyChanges>
        {
            new(nameof(Employee.FirstName), "James"),
            new(nameof(Employee.LastName), "Smith")
        };

        var result = propertyChanger.Update(employee, changes);

        Assert.True(result);
        Assert.Equal("James", employee.FirstName);
        Assert.Equal("Smith", employee.LastName);
    }

    /// <summary>
    /// Тест, обработчик полей должен возвращать false, при попытке сменить Id
    /// </summary>
    [Theory]
    [MemberData(nameof(GetPropertyChangers))]
    public void Update_ShouldReturnFalse_WhenTryingToUpdateId(IEmployeePropertyChanger propertyChanger)
    {
        var employee = new Employee { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        var changes = new List<IEmployeePropertyChanger.PropertyChanges>
        {
            new(nameof(Employee.Id), "124"),
            new(nameof(Employee.FirstName), "James")
        };

        var result = propertyChanger.Update(employee, changes);

        Assert.False(result);
    }

    /// <summary>
    /// Тест, обработчик полей должен возвращать false, при неверном указании поля
    /// </summary>
    [Theory]
    [MemberData(nameof(GetPropertyChangers))]
    public void Update_ShouldReturnFalse_WhenInvalidPropertyName(IEmployeePropertyChanger propertyChanger)
    {
        var employee = new Employee { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        var changes = new List<IEmployeePropertyChanger.PropertyChanges>
        {
            new("InvalidProperty", "Value"),
            new(nameof(Employee.FirstName), "James")
        };

        var result = propertyChanger.Update(employee, changes);
        Assert.False(result);
    }

    /// <summary>
    /// Тест, обработчик полей должен пропускать повторяющиеся поля при обновлении
    /// </summary>
    [Theory]
    [MemberData(nameof(GetPropertyChangers))]
    public void Update_ShouldSkipDuplicateFields_WhenUpdating(IEmployeePropertyChanger propertyChanger)
    {
        var employee = new Employee { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        var changes = new List<IEmployeePropertyChanger.PropertyChanges>
        {
            new(nameof(Employee.FirstName), "James"),
            new(nameof(Employee.FirstName), "John")
        };

        var result = propertyChanger.Update(employee, changes);

        Assert.True(result);
        Assert.Equal("James", employee.FirstName);
    }

    /// <summary>
    /// Поставляет обработчики полей в тесты
    /// </summary>
    /// <returns>Обработчики поля в массиве</returns>
    public static IEnumerable<object[]> GetPropertyChangers()
    {
        yield return new object[] { new ReflectionPropertyChanger() };
        yield return new object[] { new ConditionalEmployeePropertyChanger() };
    }
}