using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager
{
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            UpdateDataGrid();
        }

        private async void LoadData()
        {
            ShowLoading();
            await Task.Run(() =>
            {
                employees = EmployeeDataManager.LoadData();
            });
            UpdateDataGrid();
            HideLoading();
        }

        private void RefreshData(object sender, RoutedEventArgs e)
        {
            LoadData(); // Перезагружаем данные
        }

        private void ShowAddEmployeeBlock(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Visible;
            AddManagerPanel.Visibility = Visibility.Collapsed;
            ChangeSalaryPanel.Visibility = Visibility.Collapsed;
            ClearEmployeeFields();
        }

        private void ShowAddManagerBlock(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddManagerPanel.Visibility = Visibility.Visible;
            ChangeSalaryPanel.Visibility = Visibility.Collapsed;
            ClearManagerFields();
        }

        private void ShowChangeSalaryBlock(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddManagerPanel.Visibility = Visibility.Collapsed;
            ChangeSalaryPanel.Visibility = Visibility.Visible;
            ClearSalaryFields();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            string searchTerm = txtSearch.Text.Trim().ToLower();
            List<Employee> filteredEmployees = null;

            await Task.Run(() =>
            {
                filteredEmployees = employees.Where(emp =>
                    emp.FirstName.ToLower().Contains(searchTerm) || emp.LastName.ToLower().Contains(searchTerm)).ToList();
            });

            UpdateDataGrid(filteredEmployees);
            HideLoading();
        }

        private async void ShowData(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            await Task.Run(() => { /* Выполнение длительных операций при обновлении данных, если потребуется */ });
            UpdateDataGrid(employees);
            HideLoading();
        }

        private async void ClearData(object sender, RoutedEventArgs e)
        {
            ShowLoading();
            await Task.Run(() => { employees.Clear(); });
            MessageBox.Show("Все данные очищены.");

            UpdateDataGrid();
            EmployeeDataManager.SaveData(employees); // Передаем список для сохранения
            HideLoading();
        }

        private async void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.TryParse(txtEmployeeSalary.Text, out decimal salary) &&
                Decimal.TryParse(txtEmployeeBonus.Text, out decimal bonus))
            {
                Employee newEmployee = new Employee(
                    txtEmployeeLastName.Text.Trim(),
                    txtEmployeeFirstName.Text.Trim(),
                    txtEmployeeMiddleName.Text.Trim(),
                    txtEmployeePosition.Text.Trim(),
                    salary,
                    bonus);
                ShowLoading();
                await Task.Run(() => { employees.Add(newEmployee); });
                UpdateDataGrid();
                EmployeeDataManager.SaveData(employees); // Передаем список для сохранения
                ClearEmployeeFields();
                MessageBox.Show("Сотрудник добавлен успешно!");
                HideLoading();
            }
            else
            {
                MessageBox.Show("Введите корректные значения для зарплаты и бонуса.");
            }
        }

        private async void AddManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.TryParse(txtManagerSalary.Text, out decimal salary) &&
                Decimal.TryParse(txtManagerBonus.Text, out decimal bonus))
            {
                // Предполагается, что Manager - это подкласс Employee
                Employee newManager = new Employee(
                    txtManagerLastName.Text.Trim(),
                    txtManagerFirstName.Text.Trim(),
                    txtManagerMiddleName.Text.Trim(),
                    txtManagerPosition.Text.Trim(),
                    salary,
                    bonus);
                ShowLoading();
                await Task.Run(() => { employees.Add(newManager); });
                UpdateDataGrid();
                EmployeeDataManager.SaveData(employees); // Передаем список для сохранения
                ClearManagerFields();
                MessageBox.Show("Менеджер добавлен успешно!");
                HideLoading();
            }
            else
            {
                MessageBox.Show("Введите корректные значения для зарплаты и бонуса.");
            }
        }

        private async void ChangeSalaryButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = txtChangeSalaryLastName.Text.Trim();
            string firstName = txtChangeSalaryFirstName.Text.Trim();
            string middleName = txtChangeSalaryMiddleName.Text.Trim();

            if (Decimal.TryParse(txtChangeSalaryPercentage.Text, out decimal salaryPercentage) &&
                Decimal.TryParse(txtChangeBonusPercentage.Text, out decimal bonusPercentage)) // Изменяем на два процента
            {
                ShowLoading();
                Employee employee = null;

                await Task.Run(() =>
                {
                    employee = employees.FirstOrDefault(emp =>
                        emp.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                        emp.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                        emp.MiddleName.Equals(middleName, StringComparison.OrdinalIgnoreCase));
                });

                if (employee != null)
                {
                    employee.IncreaseSalary(salaryPercentage); // Увеличиваем зарплату

                    employee.Bonus += employee.Bonus * bonusPercentage / 100; // Увеличиваем бонус отдельно
                    MessageBox.Show($"Зарплата для {employee.FirstName} {employee.LastName} увеличена на {salaryPercentage}%. Бонус увеличен на {bonusPercentage}%.");
                    UpdateDataGrid();
                    EmployeeDataManager.SaveData(employees); // Сохраняем изменения
                }
                else
                {
                    MessageBox.Show($"Сотрудник с именем '{firstName} {lastName}' не найден.");
                }

                ClearSalaryFields();
                HideLoading();
            }
            else
            {
                MessageBox.Show("Введите корректные значения для процентов увеличения зарплаты и бонуса.");
            }
        }

        private void UpdateDataGrid(List<Employee> filteredEmployees = null)
        {
            employeeDataGrid.ItemsSource = filteredEmployees ?? employees;
        }

        private void ShowLoading()
        {
            LoadingPanel.Visibility = Visibility.Visible;
            this.IsEnabled = false; // Блокируем основное окно
        }

        private void HideLoading()
        {
            LoadingPanel.Visibility = Visibility.Collapsed;
            this.IsEnabled = true; // Разблокируем основное окно
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox != null && (textBox.Text != string.Empty))
            {
                textBox.Clear();
            }
        }

        private void ClearEmployeeFields()
        {
            txtEmployeeLastName.Text = "Фамилия";
            txtEmployeeFirstName.Text = "Имя";
            txtEmployeeMiddleName.Text = "Отчество";
            txtEmployeePosition.Text = "Должность";
            txtEmployeeSalary.Text = "Зарплата";
            txtEmployeeBonus.Text = "Бонус";
        }

        private void ClearManagerFields()
        {
            txtManagerLastName.Text = "Фамилия";
            txtManagerFirstName.Text = "Имя";
            txtManagerMiddleName.Text = "Отчество";
            txtManagerPosition.Text = "Должность";
            txtManagerSalary.Text = "Зарплата";
            txtManagerBonus.Text = "Бонус";
        }

        private void ClearSalaryFields()
        {
            txtChangeSalaryLastName.Text = "Фамилия";
            txtChangeSalaryFirstName.Text = "Имя";
            txtChangeSalaryMiddleName.Text = "Отчество";
            txtChangeSalaryPercentage.Text = "Процент увеличения зарплаты";
            txtChangeBonusPercentage.Text = "Процент увеличения бонуса";
        }
    }
}
