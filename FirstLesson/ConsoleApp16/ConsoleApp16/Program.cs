using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string func=Console.ReadLine();
            string[] lines = File.ReadAllLines(s, System.Text.Encoding.Default);
            string[] b;
            List<Student> ab = new List<Student>();
            foreach(string a in lines)
            {
                b = a.Split(';');
                ab.Add(new Student(b[0], b[1], b[2], b[3], b[4])); 
            }
            ab.RemoveAt(0);
            switch (func)
            {
                case "count":
                    count(ab);
                    break;
                case "dorm":
                    dorm(ab);
                    break;
                case "course":
                    course(ab);
                    break;
            }


        }

        private static int count(List<Student> ab)
        {
            return ab.Count();
        }

        private static void dorm(List<Student> ab)
        {
            List<String> q = new List<String>();
            foreach (Student a in ab)
            {
                if(a.dorm)
                {
                    q.Add(a.fio);
                }

            }
        }

        private static void course(List<Student> ab)
        {
            int[] c = new int[] { 0,0,0,0};
            foreach (Student a in ab)
            {
                c[a.course - 1]++;
            }
        }
    }
}
