using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class AutoPark
    {
        private List<Car> _cars = new List<Car>();
        private List<User> _users = new List<User>();
        private List<Rent> _rents = new List<Rent>();
        private User _TO;
        public void AddCar(string mark)
        {
            Car adding = new Car(new Guid(), mark);
            _cars.Add(adding);
        }

        public void AddUser(string name)
        {
            User adding = new User(new Guid(), name);
            _users.Add(adding);
        }

        public bool RentCar(User user, Car car,  DateTime start, DateTime finish)
        {
            var index1 = _cars.IndexOf(car);
            var index2 = _users.IndexOf(user);

            if ((index1== -1)||(index2 == -1)) return false;

            foreach (var rent in _rents)
            {
                if ((rent.Tenant == user) && ((!(((start > rent.Start) && (finish > rent.Finish)) || ((start < rent.Start) && (finish < rent.Finish)))))) return false;
            }

            Rent adding = new Rent(start, finish, user, car);
            _rents.Add(adding);

            if (car.WantToRest())
            {
                SendToTO(car);
            }

            return true;
        }

        private void SendToTO(Car car)
        {
            DateTimeOffset last = new DateTimeOffset(0, 0, 0, 0, 0, 0, new TimeSpan(0));
            foreach (var rent in _rents)
            {
                if ((rent.CarMark == car) && (rent.Finish > last)) last = rent.Finish;
            }
            Rent adding = new Rent(new DateTimeOffset(0, 0, 0, 0, 0, 0, new TimeSpan(0)), last, _TO, car);
            _rents.Add(adding);
        }

        public List<Rent> ShowUserRents(User user)
        {
            List<Rent> UserRents = new List<Rent>();
            foreach (var rent in _rents)
            {
                if (rent.Tenant == user) UserRents.Add(rent);
            }
            return UserRents;
        }

        public List<Car> ShowAviableCars(DateTimeOffset start, DateTimeOffset finish)
        {
            List<Car> AviableCars = _cars;
            foreach (var rent in _rents)
            {
                if (!(((start > rent.Start) && (finish > rent.Finish)) || ((start < rent.Start) && (finish < rent.Finish))))
                    AviableCars.Remove(rent.CarMark);
            }
            return AviableCars;
        }

        public AutoPark (List<Car> cars, List<User> users, List<Rent> rents)
        {
            _cars = cars;
            _users = users;
            _rents = rents;
            User TO = new User(new Guid(), "checkup");
            _TO = TO;
            _users.Add(_TO);
        }

    }
}
