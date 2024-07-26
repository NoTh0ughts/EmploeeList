using EmployeeList.AppConstants;
using EmployeeList.Command;
using EmployeeList.DataProvider;
using EmployeeList.DataProvider.FileSource;
using EmployeeList.Model;

namespace EmployeeList;

/// <summary>
/// Программа для взаимодействия с JSON - файлом сотрудников.
/// Поддерживает операции:
///     -add, -update, -getall, -get, -delete.
/// Взаимодействие с файлом реализуется через <see cref="IDataSource"/>,
/// данные из которого получает <see cref="IRepository{T}"/>
/// В методе Main производится вся логика инициализации приложения и запуск комманд
/// <remarks>Подробнее в README.md</remarks>
/// </summary>
internal static class Program
{
    public static int Main(string[] args)
    {
        // В программу не передали аргументов, продолжение невозможно
        if (args.Length == 0)
        {
            Console.WriteLine("Args is not found, please check input");
            return AppConstant.ExitCodes.HAVE_NO_ARGS;
        }

        const string jsonFilePath = @"C:\Users\NoThoughts\RiderProjects\EmploeeList\EmployeeList\Test.json";
        try
        {
            IDataSource dataSource = new LocalFileDataSource();
            using IRepository<Employee> dataProvider = new JsonDataProvider(jsonFilePath, dataSource);

            return args[0] switch
            {
                "-update" => CommandHandler.ExecuteUpdateCommand(dataProvider, args[1..]),
                "-add"    => CommandHandler.ExecuteAddCommand   (dataProvider, args[1..]),
                "-get"    => CommandHandler.ExecuteGetCommand   (dataProvider, args[1]),
                "-delete" => CommandHandler.ExecuteDeleteCommand(dataProvider, args[1]),
                "-getall" => CommandHandler.ExecuteGetAllCommand(dataProvider),
                _         => CommandHandler.HandleUnknownCommand(args[0])
            };
        }
        catch (FileNotFoundException fe)
        {
            Console.WriteLine($"File not found: {fe.Message}");
            return AppConstant.ExitCodes.INVALID_PATH;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return AppConstant.ExitCodes.ERROR;
        }
    }
}