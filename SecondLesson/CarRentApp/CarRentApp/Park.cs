using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarRentApp
{
    public class Park
    {
        public Park(List<Car> cars)
        {
            Cars = cars ?? throw new ArgumentNullException(nameof(cars));
        }

        public List<Car> Cars { get; set; }
        public List<Car> GetFreeCars(DateTimeOffset[] wishfulDate)
        {
            List<Car> FreeCar = new List<Car>();
            bool check = true;
            foreach (Car car in Cars)
            {
                foreach (DateTimeOffset[] carRentTime in car.ListOfCar)
                {
                    if ((carRentTime[1] > wishfulDate[0]) ||
                        (wishfulDate[1] < carRentTime[0]))
                    {
                        check = false;
                    }
                }
                if (check)
                {
                    FreeCar.Add(car);
                }
            }
            return FreeCar;
        }
        public void RentCar(User user, Car car, DateTimeOffset[] promisingDate)
        {
            if (user.CheckPeriod(promisingDate) && (car.CheckPeriod(promisingDate)) && (car.CheckRepair(promisingDate, car.Repair)))
            {
                user.ListOfUser.Add(promisingDate);
                car.ListOfCar.Add(promisingDate);
                car.AddOnePointToRepair();
                RentOfUser UserHistory = new RentOfUser(car.Name, promisingDate);
                user.HistoryOfUser.Add(UserHistory);
            }
        }
        public void AddNewCar(string name)
        {

            List<DateTimeOffset[]> list = new List<DateTimeOffset[]>();
            Car car = new Car(name, list);
            Cars.Add(car);
        }

    }
}
