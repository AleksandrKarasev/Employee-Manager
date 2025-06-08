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
                // Создаем 3 объекта с различными конструкторами
                if (employees.Count < 3)
                {
                    if (employees.Count == 0)
                    {
                        // Первый объект с полным конструктором
                        employees.Add(new Employee(name, position, salary));
                    }
                    else if (employees.Count == 1)
                    {
                        // Второй объект с двумя параметрами
                        employees.Add(new Employee(name, position));
                        employees[1].Salary = salary; // Устанавливаем зарплату вручную
                    }
                    else if (employees.Count == 2)
                    {
                        // Третий объект с пустым конструктором
                        Employee employee = new Employee();
                        employee.Name = name;
                        employee.Position = position;
                        employee.Salary = salary;
                        employees.Add(employee);
                    }

                    UpdateEmployeeList();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Максимум 3 сотрудника.");
                }
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

        private void btnShowSalary_Click(object sender, RoutedEventArgs e)
        {
            if (lstEmployees.SelectedItem is Employee selectedEmployee)
            {
                MessageBox.Show($"Зарплата {selectedEmployee.Name}: {selectedEmployee.Salary:C}");
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для показа зарплаты.");
            }
        }

        private void btnChangeSalary_Click(object sender, RoutedEventArgs e)
        {
            if (lstEmployees.SelectedItem is Employee selectedEmployee)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Введите новую зарплату:", "Изменить зарплату", selectedEmployee.Salary.ToString());

                if (decimal.TryParse(input, out decimal newSalary))
                {
                    selectedEmployee.Salary = newSalary;
                    UpdateEmployeeList();
                    MessageBox.Show($"Зарплата для {selectedEmployee.Name} изменена на: {selectedEmployee.Salary:C}");
                }
                else
                {
                    MessageBox.Show("Введите корректное значение зарплаты.");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для изменения зарплаты.");
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
