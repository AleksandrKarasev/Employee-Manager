using System;

namespace EmployeeManager
{
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        // Конструктор без параметров
        public Employee() { }

        // Конструктор с параметрами
        public Employee(string name, string position)
        {
            Name = name;
            Position = position;
        }

        // Конструктор с параметрами
        public Employee(string name, string position, decimal salary)
        {
            Name = name;
            Position = position;
            Salary = salary;
        }

        public void IncreaseSalary(decimal percentage)
        {
            Salary += Salary * percentage / 100;
        }

        public override string ToString()
        {
            return $"{Name}, {Position}, Зарплата: {Salary:C}";
        }
    }
}
