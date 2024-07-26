using EmployeeList.AppConstants;
using EmployeeList.DataProvider;
using EmployeeList.Model;

namespace EmployeeList.Command;


/// <summary>
/// Производит обработку введенных данных и передает их в обработку к командам <see cref="BaseCommand"/>
/// </summary>
public static class CommandHandler
{
    /// <summary>
    /// Выполняет добавление новой записи
    /// Создает команду <see cref="AddCommand"/> и передает данные в нее
    /// Поддерживает любые порядки аргументов inputData
    /// </summary> 
    /// <param name="dataProvider">Провайдер данных</param>
    /// <param name="inputData">Входные данные в формате "FirstName:Ryan LastName:Gosling"</param>
    /// <returns>Код выполнения - выхода</returns>
    public static int ExecuteAddCommand(IRepository<Employee> dataProvider, string[] inputData)
    {
        if (inputData.Length != 0)
        {
            var addCommand = new AddCommand(dataProvider) { Changes = inputData };
            return addCommand.Execute() ? AppConstant.ExitCodes.OK : AppConstant.ExitCodes.CANNOT_ADD;
        }

        Console.WriteLine("Cannot add new employee - not enough data");
        return AppConstant.ExitCodes.INVALID_ARGS;
    }

    /// <summary>
    /// Выполняет обновление записи
    /// Конвертирует в необходимый формат и передает в <see cref="UpdateCommand"/>
    /// Поддерживает любые порядки аргументов inputData
    /// </summary>
    /// <param name="dataProvider">Провайдер данных</param>
    /// <param name="inputData">Входные данные в формате "FirstName:Ryan LastName:Gosling"</param>
    /// <returns>Код выполнения - выхода</returns>
    public static int ExecuteUpdateCommand(IRepository<Employee> dataProvider, string[] inputData)
    {
        // Поиск строки с полем id в аргументах
        var idEntry = inputData.FirstOrDefault(x => x.StartsWith("Id" + AppConstant.Input.ARGS_SEPARATOR));
        if (idEntry == null)
        {
            Console.WriteLine("ID not found in args data.");
            return AppConstant.ExitCodes.INVALID_ARGS;
        }

        var idPair = idEntry.Split(AppConstant.Input.ARGS_SEPARATOR);
        if (idPair.Length != 2 || false == int.TryParse(idPair[1].Trim(), out var id))
        {
            Console.WriteLine("Invalid or missing ID value.");
            return AppConstant.ExitCodes.INVALID_ARGS;
        }

        // Изъятие из массива элемента с Id
        var changes = inputData
            .Where(x => false == x.StartsWith("Id" + AppConstant.Input.ARGS_SEPARATOR))
            .ToArray();

        if (changes.Length == 0)
        {
            Console.WriteLine("Nothing to change, please make sure that args is correct.");
            return AppConstant.ExitCodes.NOTHING_TO_CHANGE;
        }

        var command = new UpdateCommand(dataProvider)
        {
            ChangesList = changes,
            Id = id
        };

        return command.Execute() ? AppConstant.ExitCodes.OK : AppConstant.ExitCodes.CANNOT_UPDATE;
    }

    /// <summary>
    /// Выполняет получение записи (Вывод в консоль)
    /// Аллгоритм получения заложен в <see cref="GetCommand"/>
    /// </summary>
    /// <param name="dataProvider">Провайдер данных</param>
    /// <param name="inputData">Входные данные в формате Id:123"</param>
    /// <returns>Код выполнения - выхода</returns>
    public static int ExecuteGetCommand(IRepository<Employee> dataProvider, string inputData)
    {
        var idPair = inputData.Split(AppConstant.Input.ARGS_SEPARATOR);
        if (idPair is ["Id", _] && int.TryParse(idPair[1].Trim(), out var id))
        {
            var command = new GetCommand(dataProvider) { Id = id };
            return command.Execute() ? AppConstant.ExitCodes.OK : AppConstant.ExitCodes.CANNOT_GET;
        }

        Console.WriteLine("Invalid or missing ID value.");
        return AppConstant.ExitCodes.INVALID_ARGS;
    }
    
    /// <summary>
    /// Выполняет удаление записи
    /// Аллгоритм получения заложен в <see cref="DeleteCommand"/>
    /// </summary>
    /// <param name="dataProvider">Провайдер данных</param>
    /// <param name="inputData">Входные данные в формате Id:123"</param>
    /// <returns>Код выполнения - выхода</returns>
    public static int ExecuteDeleteCommand(IRepository<Employee> dataProvider, string inputData)
    {
        var idPair = inputData.Split(AppConstant.Input.ARGS_SEPARATOR);
        if (idPair is ["Id", _] && int.TryParse(idPair[1].Trim(), out var id))
        {
            var command = new DeleteCommand(dataProvider) { Id = id };
            return command.Execute() ? AppConstant.ExitCodes.OK : AppConstant.ExitCodes.CANNOT_DELETE;
        }

        Console.WriteLine("Invalid or missing ID value.");
        return AppConstant.ExitCodes.INVALID_ARGS;
    }
    
    /// <summary>
    /// Выполняет получение всех записей
    /// Аллгоритм получения заложен в <see cref="GetAllCommand"/>
    /// </summary>
    /// <param name="dataProvider">Провайдер данных</param>
    /// <returns>Код выполнения - выхода</returns>
    public static int ExecuteGetAllCommand(IRepository<Employee> dataProvider)
    {
        var command = new GetAllCommand(dataProvider);
        return command.Execute() ? AppConstant.ExitCodes.OK : AppConstant.ExitCodes.CANNOT_GETALL;
    }
    
    /// <summary>
    /// Вызывается при некорректном вводе аргументов
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static int HandleUnknownCommand(string command)
    {
        Console.WriteLine($"Unknown command: {command}, available commands: -add -get -delete -update -getall");
        return AppConstant.ExitCodes.UNKNOWN_COMMAND;
    }
}