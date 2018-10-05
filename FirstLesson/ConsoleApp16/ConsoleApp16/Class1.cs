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
        string fio;
        int course;
        string institut;
        bool dorm;
        public string FIO
        {
            get {return fio; }            
        }
        public int Course
        {
            get { return course; }
        }
        public bool Dorm
        {
            get { return dorm; }
        }

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
            this.course = course[0] - '0';
            this.institut = institut;
            this.dorm = dorm.Equals("Да"); ;

        }
    }
}
