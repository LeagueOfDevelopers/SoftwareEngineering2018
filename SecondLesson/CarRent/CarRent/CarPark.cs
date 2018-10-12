using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRent
{
    public class CarPark
    {
        public string Name { get; private set; }
        public List<Car> Cars { get; private set; } = new List<Car>();

        // times a car can go without maintenance
        public static int CarWorkingTimes { get; } = 10;

        //лист из записей
        private List<Record> RecordsJournal { get; set; } = new List<Record>();

        private List<Guid> RepairIds { get; set; } = new List<Guid>();

        public CarPark(string name, List<Car> cars)
        {
            this.Name = name;
            AddCar(cars);
        }

        public CarPark(string name)
        {
            this.Name = name;
        }

        public void AddCar(Car car)
        {
            this.Cars.Add(car);

            Period InitPeriod = Period.ZeroPeriod();
            RepairCar(car, InitPeriod);
        }
        //override with list
        public void AddCar(List<Car> carList)
        {
            //this.Cars.AddRange(carList);
            foreach(Car car in carList)
            {
                AddCar(car);
                
            }
        }
        //override with array
        public void AddCar(Car[] cars)
        {
            //this.Cars.AddRange(cars);
            foreach (Car car in cars)
            {
                AddCar(car);
            }
        }

        public bool RentCar(Car car, DateTimeOffset startDate, DateTimeOffset endDate, User user)
        {
            //TODO решить проблему с отбрасыванием времени
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
            Record RepairRecord = new Record(car, period);

            RecordsJournal.Add(RepairRecord);

            RepairIds.Add(RepairRecord.Id);
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

            List<Record> AvailableRecords =
                     RecordsJournal.FindAll(
                    (Record record) => period.NotIntersect(record.Period));

            List<Car> AvailableCars = AvailableRecords.ConvertAll((record) => record.Car);

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

                Id = Guid.NewGuid();

                
            }

            public Record(Car car, Period period, User user)
            {
                this.Car = car;
                this.Period = period;
                this.User = user;
                Id = Guid.NewGuid();
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

        //may be written like method not class
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

            public static Period ZeroPeriod()
            {
                return new Period(DateTimeOffset.Now, DateTimeOffset.Now.AddTicks(1));
            }
        }
    }
}