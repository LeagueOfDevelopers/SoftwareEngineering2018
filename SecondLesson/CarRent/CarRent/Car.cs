using System.Collections.Generic;
using System;


namespace CarRent
{
    public class Car
    {        
        public int _number;
        public Guid _id;
        public DateTimeOffset _end_date_of_last_maintenance;

        public Car(int number, Guid id, DateTimeOffset first_possible_rentdate)
        {
            _number = number;
            _id = id;
            _end_date_of_last_maintenance = first_possible_rentdate;
        }
    }
}
