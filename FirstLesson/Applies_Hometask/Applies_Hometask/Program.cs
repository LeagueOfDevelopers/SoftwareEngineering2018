using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Applies_Hometask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string FileName = "/Users/odmen/Documents/GitHub/SoftwareEngineering2018/FirstLesson/Applies_Hometask/Тестирование.csv";

            StoreOfData(FileName);

            while (true)
            {
                Console.WriteLine("Enter Command: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case ("applies"):
                        Console.WriteLine(CountApplies());
                        break;

                    case ("residence"):
                        CountDormersDisplay();
                        break;

                    case ("statistics"):
                        DisplayStatistics();
                        break;

                    default:
                        Console.WriteLine("Wrong command, try again...");
                        break;
                }
            }
        }

        public static string[] data;

        public static string[] StoreOfData(string FileName)
        {
            data = GetData(FileName);
            return data;
        }

        public static string[] GetData(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName).Skip(1).ToArray();
            return lines;
        }

        public static int CountApplies()
        {
            return data.Length;
        }

        public static List<string> CountDormers()
        {
            string[] arrayOfLines = new string[5];
            List<string> Names = new List<string>();
            foreach (var line in data)
            {
                arrayOfLines = line.Split(';');
                if (arrayOfLines[4].Contains("Да"))
                {
                    Names.Add(arrayOfLines[1]);
                }
            }
            return Names;
        }

        public static void CountDormersDisplay()
        {
            foreach(var elems in CountDormers()){
                Console.WriteLine(elems);
            }
        }
        
        public static int[] GradeStatistics()
        {
            int[] courseArray = new int[4];
            string[] arrayOfLine = new string[5];
            foreach (var line in data)
            {
                arrayOfLine = line.Split(';');
                if (arrayOfLine[2].Contains("1")) courseArray[0]++;
                if (arrayOfLine[2].Contains("2")) courseArray[1]++;
                if (arrayOfLine[2].Contains("3")) courseArray[2]++;
                if (arrayOfLine[2].Contains("4")) courseArray[3]++;
            }
            return courseArray;
        }

        public static void DisplayStatistics()
        {
            int[] courseArray = GradeStatistics();

            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine($"Applicants of {i} grade are {courseArray[i - 1]}");
            }
        }


    }
}