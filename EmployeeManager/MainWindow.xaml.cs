using System;
using System.Collections.Generic;
using System.Windows;

namespace EmployeeManager
{
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string position = txtPosition.Text;
            decimal salary;

            if (decimal.TryParse(txtSalary.Text, out salary))
            {
                Employee employee = new Employee(name, position, salary);
                employees.Add(employee);
                UpdateEmployeeList();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Введите корректную зарплату");
            }
        }

        private void btnIncreaseSalary_Click(object sender, RoutedEventArgs e)
        {
            decimal percentage;

            if (decimal.TryParse(txtIncreasePercentage.Text, out percentage) && lstEmployees.SelectedItem is Employee selectedEmployee)
            {
                selectedEmployee.IncreaseSalary(percentage);
                UpdateEmployeeList();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника и введите корректный процент увеличения.");
            }
        }

        private void UpdateEmployeeList()
        {
            lstEmployees.Items.Clear();
            foreach (var employee in employees)
            {
                lstEmployees.Items.Add(employee);
            }
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
            txtIncreasePercentage.Clear();
        }
    }
}
