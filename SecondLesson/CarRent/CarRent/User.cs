using System.Collections.Generic;
using System;

namespace CarRent
{
    public class User
    {        
        public string _first_name { get; private set; }
        public string _second_name { get; private set; }
        public Guid _id;

        public User(string first_name, string second_name, Guid id)
        {
            _first_name = first_name;
            _second_name = second_name;
            _id = id;
        }

    }
}