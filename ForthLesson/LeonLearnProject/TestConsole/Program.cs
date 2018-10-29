using System;
using System.Linq;
using LeonLearn;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Service();
            var b = a.BeginLesson(Guid.Empty);
            Console.WriteLine(b);
            var ans = Console.ReadLine();
            var boolAns = ans.Trim().Select(s =>
            {
                if (s == '1') return true;
                if (s == '0') return false;
                throw new Exception("Only 1 and 0");
            }).ToArray();

            var res = a.EndLesson(Guid.Empty, b, boolAns);

            foreach (var taskRes in res)
            {
                Console.Write(taskRes);
            }

            var inProgress = a.GetInProgressWords(Guid.Empty);
            foreach (var i in inProgress)
            {
                Console.Write(i + "\n");
            }
            
            var learned = a.GetInProgressWords(Guid.Empty);
            foreach (var i in learned)
            {
                Console.Write(i + " ");
            }
        }
    }
}