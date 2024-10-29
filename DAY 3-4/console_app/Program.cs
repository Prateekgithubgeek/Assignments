namespace Console_Core_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            LambdaDemo lambdaDemo = new LambdaDemo();
            lambdaDemo.Demo();

            List<Employee> employees = new List<Employee>()
            { new Employee{EmployeeId = 101,Name = "Anisha", Experience = 2,Salary=90000},
              new Employee{EmployeeId = 102,Name = "Swati", Experience = 3,Salary=87000},
              new Employee{EmployeeId = 103,Name = "Judhi", Experience = 2,Salary=67000},
              new Employee{EmployeeId = 104,Name = "Kashvi", Experience = 4,Salary=99000},
            };

            Console.WriteLine("\n Employee List\n ----------------");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} {employee.Name} " + $"{employee.Experience} {employee.Salary}");
            }

            var sortedEmployeeByIdDesc = employees.OrderByDescending(e => e.EmployeeId);
            Console.WriteLine("\n Employee list order by id in desc \n -------------------");
            foreach (var employee in sortedEmployeeByIdDesc) { Console.WriteLine($"{employee.EmployeeId} {employee.Name}" + $"{employee.Experience} {employee.Salary}"); }
        }
    }
}
