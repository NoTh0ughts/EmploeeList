using System.Text.Json.Serialization;

namespace EmployeeList.Model;

[JsonSerializable(typeof(Employee))]
public class Employee
{
    public int Id                { get; set; }
    public string FirstName      { get; set; }
    public string LastName       { get; set; }
    public decimal SalaryPerHour { get; set; }
}