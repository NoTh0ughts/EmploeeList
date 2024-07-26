using EmployeeList.AppConstants;
using EmployeeList.Command;
using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeTests;

public class CommandHandlerUpdateTests
{
    /// <summary>
    /// Тест, команда обновления должно возвращать код INVALID_ARGS при ненахождении Id в строке с данными
    /// </summary>
    [Fact]
    public void ExecuteUpdateCommand_ShouldReturnInvalidArgs_WhenIdNotFound()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        var inputData = new[] { "FirstName:James" };

        var result = CommandHandler.ExecuteUpdateCommand(mockRepository.Object, inputData);
        Assert.Equal(AppConstant.ExitCodes.INVALID_ARGS, result);
    }

    /// <summary>
    /// Тест, команда обновления должно возвращать код INVALID_ARGS, если не вышло распарсить Id
    /// </summary>
    [Fact]
    public void ExecuteUpdateCommand_ShouldReturnInvalidArgs_WhenIdIsInvalid()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        var inputData = new[] { "Id:abc", "FirstName:James" };

        var result = CommandHandler.ExecuteUpdateCommand(mockRepository.Object, inputData);

        Assert.Equal(AppConstant.ExitCodes.INVALID_ARGS, result);
    }

    /// <summary>
    /// Тест, команда обновления должно возвращать код NOTHING_TO_CHANGE, если аргументы не переданы
    /// </summary>
    [Fact]
    public void ExecuteUpdateCommand_ShouldReturnNothingToChange_WhenNoChangesProvided()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        var inputData = new[] { "Id:123" };

        var result = CommandHandler.ExecuteUpdateCommand(mockRepository.Object, inputData);

        Assert.Equal(AppConstant.ExitCodes.NOTHING_TO_CHANGE, result);
    }

    /// <summary>
    /// Тест, команда обновления должно возвращать код CANNOT_UPDATE, если сотрудник не найден
    /// </summary>
    [Fact]
    public void ExecuteUpdateCommand_ShouldReturnCannotUpdate_WhenEmployeeNotFound()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((Employee)null!);
        var inputData = new[] { "Id:123", "FirstName:James" };

        var result = CommandHandler.ExecuteUpdateCommand(mockRepository.Object, inputData);

        Assert.Equal(AppConstant.ExitCodes.CANNOT_UPDATE, result);
    }

    /// <summary>
    /// Тест, команда обновления должно возвращать код OK, если удалось обновить сотрудника (обычный сценарий)
    /// </summary>
    [Fact]
    public void ExecuteUpdateCommand_ShouldReturnOk_WhenEmployeeUpdatedSuccessfully()
    {
        var mockRepository = new Mock<IRepository<Employee>>();
        var employee = new Employee { Id = 123, FirstName = "John", LastName = "Doe", SalaryPerHour = new decimal(50.0) };
        mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns(employee);
        var inputData = new[] { "Id:123", "FirstName:James" };

        var result = CommandHandler.ExecuteUpdateCommand(mockRepository.Object, inputData);

        Assert.Equal(AppConstant.ExitCodes.OK, result);
        Assert.Equal("James", employee.FirstName);
    }
}