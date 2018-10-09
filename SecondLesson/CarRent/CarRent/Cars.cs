using System.Collections.Generic;


namespace CarRent
{
    class Cars
    {
        public List<Car> _list { private set; get; }

        public void AddNewCarInCarPark(Car adding_car)
        {
            _list.Add(adding_car);
        }

    }
}
