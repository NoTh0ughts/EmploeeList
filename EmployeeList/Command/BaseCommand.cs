using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;

/// <summary>
/// Базовый класс синхронной команды, инкапсулирующий взаимодействие с репозиторием <see cref="IRepository{T}"/>.
/// Предоставляет базовое поведение наследников
/// <example> Примеры реализации:
/// <see cref="AddCommand"/>
/// <see cref="GetCommand"/>
/// </example>
/// </summary>
public abstract class BaseCommand
{
    protected readonly IRepository<Employee> _repository;

    public BaseCommand(IRepository<Employee> repository) => _repository = repository;

    /// <summary>
    /// Выполняет заложенный алгоритм команды
    /// </summary>
    public abstract bool Execute();
}