using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    class Program
    {
        static void Main(string[] args)
        {

            var rentCompany = new RentCompany(new List<Car> { 
                new Car("Solaris"),
                new Car("Benz"),
                new Car("Civic"),
                new Car("Kalina")
            });

            var user = new User("Anatoly", false);

            Console.WriteLine(_dateTime);
        }

        public static DateTimeOffset _dateTime = DateTimeOffset.Now;
    }

}
