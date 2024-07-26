using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Команда удаления сотрудника,
/// Выполняет удаление сотрудника по его идентификатору
/// </summary>
public class DeleteCommand : BaseCommand
{
    public int Id { get; init; }
    
    /// <summary>
    /// Удаляет сотрудника по Id
    /// </summary>
    public override bool Execute()
    {
        var isDeleted = _repository.Delete(Id);
        
        Console.WriteLine(isDeleted ? $"Deleted item with Id = {Id}" : $"Item with Id = {Id} not found");
        return isDeleted;
    }

    public DeleteCommand(IRepository<Employee> repository) : base(repository) { }
}