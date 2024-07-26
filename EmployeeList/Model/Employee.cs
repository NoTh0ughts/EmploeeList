namespace EmployeeList.Model;

/// <summary>
/// Класс данных сотрудника, содержит основную информацию
/// </summary>
public class Employee
{
    public int     Id            { get; set; }
    public string  FirstName     { get; set; } = "";
    public string  LastName      { get; set; } = "";
    public decimal SalaryPerHour { get; set; }

    public override string ToString()
    {
        return $"Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = {SalaryPerHour}";
    }
}