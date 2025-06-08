using System.Collections.Generic;

namespace EmployeeManager
{
    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public decimal OldSalary { get; set; } // Чтобы хранить прежнюю зарплату
        public decimal Bonus { get; set; }
        public List<SalaryChange> SalaryHistory { get; } = new List<SalaryChange>(); // История изменений зарплаты

        public Employee(string lastName, string firstName, string middleName, string position, decimal salary, decimal bonus)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Position = position;
            Salary = salary;
            Bonus = bonus;
        }

        public void IncreaseSalary(decimal percentage)
        {
            OldSalary = Salary; // Сохраняем старую зарплату перед изменением
            Salary += Salary * percentage / 100;
            SalaryHistory.Add(new SalaryChange(OldSalary, Salary, percentage)); // Добавляем новое изменение в историю
        }
    }
}
