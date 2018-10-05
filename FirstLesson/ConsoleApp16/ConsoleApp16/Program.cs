using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ConsoleApp16
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string func=args[0];    
            string file_path = args[1];

            if (!isOkay(file_path))
            {
                return; }

            string[] data = File.ReadAllLines(file_path, System.Text.Encoding.Default);
            List<Student> abitur = CreateList(data);

            
            if (abitur.Count > 0)
            { abitur.RemoveAt(0);}
            
            switch (args[0])//4 теста сюда
            {
                case "count":
                    Print(count(abitur));
                    break;
                case "dorm":
                    PrintList(dorm(abitur));
                    break;
                case "course":
                    PrintArray(course(abitur));
                    break;
                default:
                    Print("ERROR");
                    return;
            }
            Console.ReadKey();


        }

        public static bool isOkay(string file_path)//тест сюда
        {
            return File.Exists(file_path) && CorrectExtention(file_path);
            
        }

        public static bool CorrectExtention(string file_path)//тест сюда
        {
            string exp = "csv";
            return exp.Equals(getFileExtension(file_path));
        }

        public static string getFileExtension(string fileName)//тест сюда
        {            
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }


        public static List<Student> CreateList(string[] data)//тест сюда
        {
            List<Student> abitur = new List<Student>();
            string[] b;
            foreach (string a in data)
            {
                b = a.Split(';');
                abitur.Add(new Student(b[0], b[1], b[2], b[3], b[4]));
            }
            return abitur;
        }

        public static void PrintArray(int[] v)//тест сюда
        {
            for(int i=0;i<v.Length;i++)
            {
                Console.WriteLine((i + 1) + " course - " + v[i] + " student(s)");
            }
        }

        public static void PrintList(List<string> list)//тест сюда
        {
            foreach (string a in list)
            {
                Console.WriteLine(a);
            }
        }

        public static void Print(object obj)//тест сюда
        {
            Console.WriteLine(obj.ToString());
        }

        public static int count(List<Student> ab)//тест сюда
        {
            return ab.Count();
        }

        public static List<string> dorm(List<Student> ab)//тест сюда
        {
            List<string> q = new List<string>();
            foreach (Student a in ab)
            {
                if(a.Dorm)
                {
                    q.Add(a.FIO);
                }

            }
            return q;
        }

        public static int[] course(List<Student> ab)//тест сюда
        {
            int[] c = new int[] {0,0,0,0};
            foreach (Student a in ab)
            {
                c[a.Course - 1]++;
            }
            return c;
        }
    }
}
