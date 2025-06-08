namespace EmployeeManager
{
    public class SalaryChange
    {
        public decimal OldSalary { get; }
        public decimal NewSalary { get; }
        public decimal PercentageChange { get; }

        public SalaryChange(decimal oldSalary, decimal newSalary, decimal percentageChange)
        {
            OldSalary = oldSalary;
            NewSalary = newSalary;
            PercentageChange = percentageChange;
        }

        public override string ToString()
        {
            return $"Изменение: {PercentageChange}%, Старое значение: {OldSalary:C}, Новое значение: {NewSalary:C}";
        }
    }
}
