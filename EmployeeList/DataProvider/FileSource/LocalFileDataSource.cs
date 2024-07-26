namespace EmployeeList.DataProvider.FileSource;

/// <summary>
/// Локальный источник данных
/// Реализует взаимодействие с файловой системой
/// </summary>
public class LocalFileDataSource : IDataSource
{
    /// <summary>
    /// Производит синхронное чтение файла и возвращает полученные данные
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    /// <returns>Данные из файла</returns>
    /// <exception cref="FileNotFoundException">Файл не найден</exception>
    public string GetRawData(string path)
    {
        if (false == File.Exists(path))
        {
            throw new FileNotFoundException($"The file {path} was not found.");
        }

        return File.ReadAllText(path);
    }

    /// <summary>
    /// Сохраняет синхронно данные в файл
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public void Save(string path, string data)
    {
        File.WriteAllText(path, data);
    }
}