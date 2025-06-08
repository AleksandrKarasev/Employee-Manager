using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManager
{
    public static class EmployeeDataManager
    {
        private const string FilePath = "employees.json";

        public static void SaveData(List<Employee> employees) // Метод принимает список сотрудников для сохранения
        {
            var json = JsonConvert.SerializeObject(employees);
            File.WriteAllText(FilePath, json);
        }

        public static List<Employee> LoadData() // Метод для загрузки данных
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<Employee>>(json);
            }

            return new List<Employee>(); // Возвращаем пустой список, если файл не существует
        }
    }
}

