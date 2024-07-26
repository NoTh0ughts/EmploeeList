namespace EmployeeList.DataProvider.FileSource;

/// <summary>
/// Интерфейс источника данных на основе синхронного кода
/// </summary>
public interface IDataSource
{
    public string GetRawData(string path);

    public void Save(string path, string data);
}