using System.Collections.Generic;
using System;


namespace CarRent
{
    class Car
    {
        List<CarRent> _tenants_list;
        int _number;

        public void RentCar(User tenant, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending )
        {
            _tenants_list.Add(new CarRent(tenant._id, date_of_begining, date_of_ending));
        }

        
    }
}
