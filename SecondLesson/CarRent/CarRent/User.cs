using System.Collections.Generic;
using System;

namespace CarRent
{
    public class User
    {
        List<UserRent> _list_of_rental_cars;
        string _first_name { get; }
        string _second_name;
        public Guid _id;

        public User(List<UserRent> list_of_rental_cars, string first_name, string second_name)
        {
            _list_of_rental_cars = list_of_rental_cars;
            _first_name = first_name;
            _second_name = second_name;
        }
        public List<UserRent> GetRentalHistory()
        {
            return _list_of_rental_cars;
        }
    }
}