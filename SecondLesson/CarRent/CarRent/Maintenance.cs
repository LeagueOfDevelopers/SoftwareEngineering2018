using System;

namespace CarRent
{
    public class Maintenance
    {
        public Car _car;
        public DateTimeOffset _date_of_begining;
        public DateTimeOffset _date_of_ending;

        public Maintenance(Car car, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            _car = car;
            _date_of_begining = date_of_begining;
            _date_of_ending = date_of_ending;
        }
    }
}
