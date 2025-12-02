namespace LinqTasks
{
    internal class Program
    {
        record Employee(int Id, string Name, int Age, int Salary, string Department = null);
        record Project(int Id, string Name, int ManagerId);

        static void Main()
        {
            var employees = new List<Employee>
            {
                new(1, "Антон", 25, 40000, "IT"),
                new(2, "Марк", 32, 65000, "IT"),
                new(3, "Иван", 35, 80000, "HR"),
                new(4, "Роман", 28, 55000, "HR"),
                new(5, "Арсений", 30, 70000, "IT"),
                new(6, "Егор", 31, 60000, "HR")
            };

            Console.WriteLine("Задание 1");
            var task1 = employees.Where(employee => employee.Age > 30 && employee.Salary > 60_000).Select(employee => new { employee.Name, employee.Salary });
            foreach (var employee in task1)
                Console.WriteLine($"{employee.Name} – {employee.Salary}$");

            Console.WriteLine("\nЗадание 2");
            var avgTop3 = employees.OrderByDescending(employee => employee.Salary).Take(3).Average(employee => employee.Salary);
            Console.WriteLine($"Средняя зп у трех самых высокооплачиваемых сотрудников: {avgTop3}$");

            Console.WriteLine("\nЗадание 3");
            var task3 = employees.GroupBy(employee => employee.Department)
                .Select(group => new
                {
                    Department = group.Key,
                    MaxSalary = group.Max(employee => employee.Salary)
                });
            foreach (var group in task3)
                Console.WriteLine($"В отделе {group.Department}, макс зп - {group.MaxSalary}$");

            Console.WriteLine("\nЗадание 4");
            var projects = new List<Project>
            {
                new(101, "Alpha", 2),   
                new(102, "Beta", 3),   
                new(103, "Gamma", 2)    
            };
            var task4 = employees.Join(projects,employee => employee.Id,project => project.ManagerId,
                                      (employee, project) => $"Менеджер: {employee.Name} - {project.Name}");
            foreach (var line in task4)
                Console.WriteLine(line);

            Console.WriteLine("\nЗадание 5");
            var found = employees.FirstOrDefault(employee => employee.Salary == 70000);
            if (found is null)
                Console.WriteLine("Не найден");
            else Console.WriteLine(found.Name);
        }
    }
}