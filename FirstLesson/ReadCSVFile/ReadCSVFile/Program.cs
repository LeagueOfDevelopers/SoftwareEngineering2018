using System;
using System.IO;
using System.Collections.Generic;

namespace ReadCSVFile
{
    public class Program
    {
        public static string[] data;

        public static void ShowAmountStatistics()
        {
            int amount = GetAmountOfStudents();
            Console.WriteLine(amount);
        }

        public static int GetAmountOfStudents()
        {
            int amount = data.Length - 1;
            return amount;
        }

        public static void ShowFromDormStatistics()
        {
            var students = GetFromDormStudents();

            foreach (string student in students)
                Console.WriteLine(student);
        }

        public static List<string> GetFromDormStudents()
        {
            var students = new List<string>();

            foreach (string line in data)
            {
                bool isDorm = GetIsDormFromLine(line);

                if (isDorm)
                {
                    string name = GetNameFromLine(line);
                    students.Add(name);
                }
            }

            return students;
        }

        public static string GetNameFromLine(string line)
        {
            var information = line.Split(';');
            var name = information[1].Trim();
            return name;
        }

        public static bool GetIsDormFromLine(string line)
        {
            var information = line.Split(';');
            var isDorm = information[4].Trim();

            if (isDorm.Equals("Да"))
                return true;
            else
                return false;
        }

        public static void ShowStatisticsCourse()
        {
            var coursesCount = GetCourseCount();

            for (int i = 0; i < coursesCount.Length; i++)
                Console.WriteLine("{0}: {1}", i + 1, coursesCount[i]);
        }

        public static int[] GetCourseCount()
        {
            int[] coursesCount = new int[4];

            for (int i = 1; i < data.Length; i++)
            {
                int course = GetCourseFromLine(data[i]);
                coursesCount[course - 1]++;
            }

            return coursesCount;
        }

        public static int GetCourseFromLine(string line)
        {
            var information = line.Split(';');
            var symbol = information[2][0].ToString();
            var course = int.Parse(symbol);
            return course;
        }

        public static void Main(string[] args)
        {
            var filepath = args[0];
            var operation = args[1];

            switch (operation)
            {
                case "amount":
                    ShowAmountStatistics();
                    break;
                case "dorm":
                    ShowFromDormStatistics();
                    break;
                case "course":
                    ShowStatisticsCourse();
                    break;
                default:
                    Console.WriteLine("Unknown operation");
                    break;
            }
        }

        public static string[] GetData(string filepath)
        {
            if (File.Exists(filepath))
                return File.ReadAllLines(filepath);
            else
                throw new FileNotFoundException("Не найден файл по пути: " + filepath);
        }
    }
}
