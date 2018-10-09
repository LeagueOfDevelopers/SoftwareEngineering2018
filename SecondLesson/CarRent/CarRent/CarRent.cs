using System;

namespace CarRent
{
    public class CarRent
    {
        Guid _tenant;
        DateTimeOffset _date_of_begining;
        DateTimeOffset _date_of_ending;

        public CarRent(Guid tenant, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            _tenant = tenant;
            _date_of_begining = date_of_begining;
            _date_of_ending = date_of_ending;
        }
    }
}