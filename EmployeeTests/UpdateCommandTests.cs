using EmployeeList.Command;
using EmployeeList.Command.UpdateProviders;
using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeTests;

public class UpdateCommandTests
{
    private readonly Mock<IRepository<Employee>> _mockRepository = new();

    /// <summary>
    /// Тест, комманда обновления должна совершать откат при провале обновления
    /// </summary>
    [Fact]
    public void Execute_UpdateFails_RollbackChanges()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 100.0m };
        
        // Определяем изменения и настраиваем поведение мока
        var changes = new[] { "FirstName:James" };
        mockRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(employee);
        mockRepository.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Employee>()));

        // Создаем команду обновления и устанавливаем зависимость
        var updateCommand = new UpdateCommand(mockRepository.Object) { Id = 1, ChangesList = changes };
        var mockPropertyChanger = new Mock<IEmployeePropertyChanger>();
        mockPropertyChanger.Setup(pc => pc.Update(It.IsAny<Employee>(),
                It.IsAny<IEnumerable<IEmployeePropertyChanger.PropertyChanges>>()))
            .Returns(false);

        updateCommand.PropertyChanger = mockPropertyChanger.Object;
        
        var result = updateCommand.Execute();
        
        // Проверяем, что метод Get был вызван один раз и Update был вызван с исходными данными
        Assert.False(result);
        mockRepository.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        mockRepository.Verify(
            r => r.Update(It.IsAny<int>(),
                It.Is<Employee>(e => e.FirstName == "John" && e.LastName == "Doe" && e.SalaryPerHour == 100.0m)),
            Times.Once);
    }

    /// <summary>
    /// Тест, комманда обновления должна возвращать true, если сотрудник обновлен успешно
    /// </summary>
    [Fact]
    public void UpdateCommand_ShouldReturnTrue_WhenEmployeeUpdatedSuccessfully()
    {
        var employee = new Employee
            { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(employee);

        var changes = new[] { "FirstName:James", "LastName:Smith" };
        var command = new UpdateCommand(_mockRepository.Object)
        {
            Id = 123,
            ChangesList = changes
        };

        var result = command.Execute();

        Assert.True(result);
        Assert.Equal("James", employee.FirstName);
        Assert.Equal("Smith", employee.LastName);
    }

    /// <summary>
    /// Тест, комманда обновления должна возвращать false, если сотрудник не найден
    /// </summary>
    [Fact]
    public void UpdateCommand_ShouldReturnFalse_WhenEmployeeNotFound()
    {
        _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((Employee)null);

        var changes = new[] { "FirstName:James" };
        var command = new UpdateCommand(_mockRepository.Object)
        {
            Id = 123,
            ChangesList = changes
        };

        var result = command.Execute();
        Assert.False(result);
    }

    /// <summary>
    /// Тест, комманда обновления должна возвращать false, при попытке обновить Id
    /// </summary>
    [Fact]
    public void UpdateCommand_ShouldReturnFalse_WhenTryingToUpdateId()
    {
        var employee = new Employee
            { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(employee);

        var changes = new[] { "Id:124", "FirstName:James" };
        var command = new UpdateCommand(_mockRepository.Object)
        {
            Id = 123,
            ChangesList = changes
        };

        var result = command.Execute();
        Assert.False(result);
    }

    /// <summary>
    /// Тест, комманда обновления должна возвращать false, если свойство не найдено
    /// </summary>
    [Fact]
    public void UpdateCommand_ShouldReturnFalse_WhenInvalidPropertyName()
    {
        var employee = new Employee
            { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        _mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(employee);

        var changes = new[] { "InvalidProperty:Value", "FirstName:James" };
        var command = new UpdateCommand(_mockRepository.Object)
        {
            Id = 123,
            ChangesList = changes
        };

        var result = command.Execute();
        Assert.False(result);
    }
}