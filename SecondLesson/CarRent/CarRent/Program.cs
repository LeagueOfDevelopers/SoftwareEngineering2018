using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class CarParkManager
    {
        private CarPark CarPark { get; set; }

        public CarParkManager(string CarParkName)
        {
            CarPark = new CarPark(CarParkName);
        }

        public string RentCar(Car car, DateTimeOffset startDate, DateTimeOffset endDate, User user)
        {
            bool result = CarPark.RentCar(car, startDate, endDate, user);

            if (result == true)
            {
                return "Car rented";
            }
            else
            {
                //Можно ввести коды ошибок
                return "Inappropriate period or user";
            }
        }

        public string AddCar(Car car)
        {
            CarPark.AddCar(car);
            return "Car Added";
        }

        public string GetUserRecords(User user)
        {
            return String.Join('\n', CarPark.GetUserStringRecords(user));
        }

        public string GetAvailableCars(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            string[] CarsArray = CarPark.GetAvailableCarsList(startDate, endDate)
                                        .ConvertAll((Car car) => car.ToString()).ToArray();

            string Cars = String.Join('\n', CarsArray);

            return Cars;
        }

    }

    public class CarPark
    {
        public string Name { get; private set; }
        public List<Car> Cars { get; private set; }

        // times a car can go without maintenance
        public static int CarWorkingTimes { get; } = 10;

        //лист из записей
        private List<Record> RecordsJournal { get; set; } = new List<Record>();

        private List<Guid> RepairIds { get; set; } = new List<Guid>();

        public CarPark(string name, List<Car> cars)
        {
            this.Name = name;
            this.Cars = cars;
        }

        public CarPark(string name)
        {
            this.Name = name;
            this.Cars = new List<Car>();
        }

        public void AddCar(Car car)
        {
            this.Cars.Add(car);
        }
        //override with list
        public void AddCar(List<Car> carList)
        {
            this.Cars.AddRange(carList);
        }
        //override with array
        public void AddCar(Car[] cars)
        {
            this.Cars.AddRange(cars);
        }

        public bool RentCar(Car car, DateTimeOffset startDate, DateTimeOffset endDate, User user)
        {
            Period period = new Period(startDate, endDate);

            //check car availability
            if (!CarIsAvailable(car, period)) { return false; }
            //check user availability
            if (!UserIsAllowed(user, period)) { return false; }

            RecordsJournal.Add(new Record(car, period, user));

            ManageRepair(car);

            return true;
        }

        private void RepairCar(Car car, Period period)
        {
            RecordsJournal.Add(new Record(car, period));
        }

        private bool CarIsAvailable(Car car, Period period)
        {
            List<Record> ThisCarRecords = FindCarRecords(car);
            bool available = ThisCarRecords.TrueForAll((Record record) => record.Period.NotIntersect(period));
            return available;
        }

        private List<Record> FindCarRecords(Car car)
        {
            return RecordsJournal.FindAll((Record record) => record.Car == car);
        }

        private bool UserIsAllowed(User user, Period period)
        {
            List<Record> ThisUserRecords = RecordsJournal.FindAll((Record record) => record.User == user);
            bool allowed = ThisUserRecords.TrueForAll((Record record) => record.Period.NotIntersect(period));
            return allowed;
        }

        private void ManageRepair(Car car)
        {
            //find all records of a car
            List<Record> ThisCarRecords = FindCarRecords(car);

            List<Record> RepairRecords = ThisCarRecords.FindAll((Record record) => RepairIds.Contains(record.Id));

            //RepairRecords.Sort((Record x, Record y) => x.Period.EndDate.CompareTo(y.Period.EndDate));

            //конец последнего ТО
            DateTimeOffset LastRepairRecord = RepairRecords.Max((Record record) => record.Period.EndDate);
            //ситаем сколько записей было полсе ТО
            //то есть сколько дат начала/конца после конца последнего ТО
            List<Record> RecordsAfterMeintenance = ThisCarRecords.FindAll((Record record) => record.Period.EndDate > LastRepairRecord);

            if (RecordsAfterMeintenance.Count < CarWorkingTimes)
            {
                //Do nothing
                return;
            }

            DateTimeOffset LastRentEndDate = RecordsAfterMeintenance.Max((Record record) => record.Period.EndDate);
            Period RepairPeriod = new Period(LastRentEndDate.AddDays(1), LastRentEndDate.AddDays(11));
            RepairCar(car, RepairPeriod);
        }

        public List<Car> GetAvailableCarsList(DateTimeOffset date)
        {
            List<Record> UnavailableRecords = RecordsJournal.FindAll((Record record) => record.Period.StartDate < date && record.Period.EndDate > date);
            List<Car> UnavailableCars = UnavailableRecords.ConvertAll((record) => record.Car);
            List<Car> AvailableCars = Cars.FindAll((Car car) => !UnavailableCars.Contains(car));

            return AvailableCars;
        }

        public List<Car> GetAvailableCarsList(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            Period period = new Period(startDate, endDate);

            List<Record> UnavailableRecords =
                     RecordsJournal.FindAll(
                    (Record record) => period.NotIntersect(record.Period));

            List<Car> UnavailableCars = UnavailableRecords.ConvertAll((record) => record.Car);
            List<Car> AvailableCars = Cars.FindAll((Car car) => !UnavailableCars.Contains(car));

            return AvailableCars;
        }

        private List<Record> GetUserRecords(User user)
        {
            return RecordsJournal.FindAll((Record record) => record.User == user);
        }

        public string[] GetUserStringRecords(User user)
        {
            List<Record> UserRecords = GetUserRecords(user);
            string[] UserStringRecords = UserRecords.ConvertAll((Record record) => record.ToString()).ToArray();
            return UserStringRecords;
        }

        private class Record
        {
            public Car Car;
            public Period Period;
            public User User;
            public Guid Id;

            public Record(Car car, Period period)
            {
                this.Car = car;
                this.Period = period;
                User = null;
                //User = new User.EmptyUser;

                Id = new Guid();
            }

            public Record(Car car, Period period, User user)
            {
                this.Car = car;
                this.Period = period;
                this.User = user;
                Id = new Guid();
            }

            public override string ToString()
            {
                return "Car name: " + Car.Name
                                         + ". Car ID: " + Car.Id.ToString()
                                         + ". Start date: " + Period.StartDate.ToString("d")
                                         + ". End date: " + Period.EndDate.ToString("d")
                                         + ". Record ID: " + Id.ToString()
                                         + ". Reason: " + (User != null ? "user" : "repair");
            }
        }

        private class Period
        {
            public DateTimeOffset StartDate;
            public DateTimeOffset EndDate;
            //public DateTimeOffset[] TimePeriod { get; private set; } = new DateTimeOffset[2]; 
            public Period(DateTimeOffset startDate, DateTimeOffset endDate)
            {
                StartDate = startDate;
                EndDate = endDate;
            }

            public bool NotIntersect(Period otherPeriod)
            {
                return EndDate < otherPeriod.StartDate || StartDate > otherPeriod.EndDate;
            }
        }
    }

    public class Car
    {
        public string Name { get; private set; }
        public Guid Id { get; }

        public Car(string name)
        {
            this.Name = name;
            this.Id = new Guid();
        }

        public override string ToString()
        {
            return Name + " " + Id.ToString();
        }

    }

    public class User
    {
        public Guid Id;
        public string Name;

        public User(string name)
        {
            Name = name;
            Id = new Guid();
        }
    }
}
