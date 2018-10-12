using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Rent
    {
        public User _tenant;
        public Car _car;
        public DateTimeOffset _begining_date;
        public DateTimeOffset _ending_date;

        public Rent(User tenant, Car car, DateTimeOffset begining_date, DateTimeOffset ending_date)
        {
            _tenant = tenant;
            _car = car;
            _begining_date = begining_date;
            _ending_date = ending_date;
        }
    }
}
