<div id="MainTitle">

# EmployeeList

</div>
<div id="SubTitle"> 

### Программа для работы с JSON файлом, содержащим список сотрудников

</div>

Консольное приложение, обрабатывающее текстовый файл, содержащий список сотрудников в формате JSON. Формат записи о сотруднике:

- столбец Id, тип в C# - int;
- столбец FirstName, тип в C# - string;
- столбец LastName, тип в C# - string;
- столбец SalaryPerHour, тип в C# - decimal.

Приложение принимает входные аргументы (в string[] args метода Main), и на их основе
выполняет соответствующую операцию. Порядок аргументов не важен.

## !!! - Примечания
- При повторном задании полей в командах -add, -update поля повторно не будут установлены
- Поля в командах -add, -update могут быть не указаны полностью, но должно быть хотя бы 1
- При указании SalaryPerHour используется точка (согласно примеру в задании)

---

## Технологический стек:
<div id="TechStack">

* .NET
* xUnit

</div>

## Как запустить?

1. Скопировать репозиторий коммандой ```git clone https://github.com/NoTh0ughts/EmploeeList EmploeeList```
2. Перейти в дирректорию проекта EmploeeList
3. Убедиться, что установленна подходящая версия .net ```dotnet --version```
4. Восстановите зависимости командой ```dotnet restore```
5. В файле Program.cs укажите правильный путь к файлу Test.json (или к любому другому)
6. Затем выполните команды: ```dotnet build``` ```dotnet run -getall```
7. Успех!

## Тестовые примеры

- ```dotnet run -- -add FirstName:John LastName:Doe SalaryPerHour:100.50```
- ```dotnet run -- -update Id:123 FirstName:James```
- ```dotnet run -- -get Id:123```
- ```dotnet run -- -delete Id:123```
- ```dotnet run -- -getall```

