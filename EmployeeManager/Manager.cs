namespace EmployeeManager
{
    public class Manager : Employee
    {
        public Manager(string lastName, string firstName, string middleName, string position, decimal salary, decimal bonus)
            : base(lastName, firstName, middleName, position, salary, bonus) { }
    }
}
