using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    class Student
    {
        string time;
        public string fio;
        public int course;
        string institut;
        public bool dorm;
        public Student(string time, string fio, int course, string institut, bool dorm)
        {
            this.time = time;
            this.fio = fio;
            this.course = course;
            this.institut = institut;
            this.dorm = dorm;
        }
        public Student(string time, string fio, string course, string institut, string dorm)
        {
            this.time = time;
            this.fio = fio;
            this.course = (int)course[0];
            this.institut = institut;
            this.dorm = dorm.Equals("Да"); ;

        }
    }
}
