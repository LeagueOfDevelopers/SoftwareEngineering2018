using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoD_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
   
            string[] lines = GetFileLines(args[0]);
            if (lines != null)
            {
                string funcName = GetFuncName(args[1]);
                switch (funcName)
                {
                    case "CountMembers":
                        string toPrint = CountMembers(lines);
                        PrintCountMembers(toPrint);
                        break;
                    case "GetDormitoryMembers":
                        List<string> dormitoryMembers = GetDormitoryMembers(lines);
                        PrintGetDormitoryMembers(dormitoryMembers);
                        break;
                    case "GetStatistic":
                        int[] members = GetStatistic(lines);
                        PrintGetStatistic(members);
                        break;
                }
            }
          
            Console.ReadKey();
        }
        public static string GetFuncName (string funcName)
        {
            return funcName;
        }
        public static string[] GetFileLines(string argument)
        {
            try
            {
                return File.ReadAllLines(argument);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Путь к файлу не найден");
                return null;
            }
        }
        public static string CountMembers(string[] lines)
        {
            return ((lines.Length - 1).ToString());
        }
        public static List<string> GetDormitoryMembers(string[] lines){
            List<string> dormitoryMembers = new List<string>();
            foreach (var word in lines)
            {
                string[] line = word.Split(';');
                if (line[4] == "Да"){
                    dormitoryMembers.Add(line[1]);
                }
            }
            return dormitoryMembers;
        }
        public static int[] GetStatistic(string[] lines)
        {
            int[] courses = { 0, 0, 0, 0 };
            for (var i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(';');
                for (var k = 0; k < line.Length; k++)
                {
                    if (line[k] == "1 бакалавриат"){
                        courses[0]++;
                    };
                    if (line[k] == "2 бакалавриат"){
                        courses[1]++;
                    };
                    if (line[k] == "3 бакалавриат"){
                        courses[2]++;
                    };
                    if (line[k] == "4 бакалавриат"){
                        courses[3]++;
                    };
                };
            }
            return courses;
        }
        public static void PrintCountMembers(string line)
        {
            Console.WriteLine("Количество заявок " + (line));
        }
        public static void PrintGetDormitoryMembers(List<string> dormitoryMembers)
        {
            foreach (var i in dormitoryMembers)
            {
                Console.WriteLine(i);
            }
        }
        public static void PrintGetStatistic(int[] members)
        {
            for (int i = 0; i < members.Length; i++)
            {
                Console.WriteLine(i + 1 + " курс: " + members[i]);
            }
        }
     }
}
