using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Facade
    {
        public Repository repa { get; private set; }

        public Facade(Repository repa)
        {
            this.repa = repa ?? throw new ArgumentNullException(nameof(repa));
        }

        public List<Car> SeeFreeCars(DateTimeOffset start, DateTimeOffset end)
        {
            List<Car> freeCars = repa.Park.Cars;
            foreach (RentContract cont in repa.Contracts)
            {
                
                if ((start < cont.EndRent) && (end > cont.StartRent)) freeCars.Remove(cont.car);
            }

            List<Car> carOnInspection = repa.Park.SeeCarsOnInspection(start, end);
            foreach (Car car in carOnInspection)
            {
                freeCars.Remove(car);
            }
            return freeCars;
        }

        public bool RentCar(Car car, User user, DateTimeOffset startRent, DateTimeOffset endRent)
        {
            
            if (!SeeFreeCars(startRent, endRent).Contains(car)) return false;
            repa.Contracts.Add(new RentContract(startRent, endRent, user, car));
            car.AddCountUsed();
            if (car.CountUsed == 10) repa.Park.GoOnInspection(car, 
                new DateTimeOffset(startRent.Year,startRent.Month,startRent.Day + 1, 0,0,0, new TimeSpan()));
            return true;
        }

        public List<RentContract> GetRentHistory(User user)
        {
            List<RentContract> history = new List<RentContract>();
            foreach(RentContract cont in repa.Contracts)
            {
                if (cont.User == user) history.Add(cont);
            }
            return history;
        }
        public void AddCar(Car car)
        {
            repa.Park.AddCar(car);
        }

        
    }
}
