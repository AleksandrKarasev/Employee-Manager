# EmployeeManager

**EmployeeManager** - это WPF приложение для управления сотрудниками, которое позволяет добавлять новых сотрудников, увеличивать их зарплату и отображать информацию о сотрудниках.

## Описание

Приложение позволяет:

- Добавлять новых сотрудников, вводя их имя, должность и зарплату.
- Увеличивать зарплату выбранного сотрудника на указанный процент.
- Просматривать список всех сотрудников и их зарплат.

## Структура проекта

Проект состоит из следующих основных компонентов:

1. **Главное окно (`MainWindow.xaml`)** - интерфейс пользователя для ввода данных, отображения сотрудников и кнопок для действий.
2. **Класс `Employee` (`Employee.cs`)** - модель данных, представляющая сотрудника, с полями для имени, должности и зарплаты.
3. **Код логики (`MainWindow.xaml.cs`)** - обработка событий и взаимодействие с данными.

### Классы

#### Employee

public class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }

    public Employee() { }

    public Employee(string name, string position)
    {
        Name = name;
        Position = position;
    }

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

- Поля:
  - Name: Имя сотрудника.
  - Position: Должность сотрудника.
  - Salary: Зарплата сотрудника.

- Методы:
  - IncreaseSalary: Увеличивает зарплату на заданный процент.

#### MainWindow

public partial class MainWindow : Window
{
    private List<Employee> employees = new List<Employee>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnAddEmployee_Click(object sender, RoutedEventArgs e) {...}

    private void btnIncreaseSalary_Click(object sender, RoutedEventArgs e) {...}

    private void UpdateEmployeeList() {...}

    private void ClearInputFields() {...}
}

- Обработчики событий:
  - btnAddEmployee_Click: Добавляет нового сотрудника в список.
  - btnIncreaseSalary_Click: Увеличивает зарплату выбранного сотрудника.

## Установка и запуск

Чтобы запустить это приложение:

1. Клонируйте репозиторий на свой компьютер:

2. Откройте проект в Visual Studio:

   - Запустите Visual Studio и выберите "Open a project" или "Open a solution".
   - Перейдите к папке с клонированным проектом и откройте файл EmployeeManager.sln.

3. Соберите проект:

   - Нажмите F5, чтобы скомпилировать и запустить приложение.
   - Убедитесь, что все зависимости установлены, и что ваше окружение настроено на использование .NET Framework (или .NET Core, если вы используете его).

4. Использование:

   - Введите имя, должность и зарплату нового сотрудника, затем нажмите кнопку "Добавить сотрудника".
   - Чтобы увеличить зарплату сотрудника, выберите его из списка и введите процент увеличения, затем нажмите кнопку "Увеличить зарплату".
