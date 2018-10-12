using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentApp
{
    public class Car
    {
        public Car(string name, List<DateTimeOffset[]> listOfCar, int repair = 0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ListOfCar = listOfCar ?? throw new ArgumentNullException(nameof(listOfCar));
            Id = _id++;
            Repair = repair;
        }
        private static int _id;
        public string Name { get; }
        public List<DateTimeOffset[]> ListOfCar { get; }
        public int Id { get; private set; }
        public int Repair { get; private set; }
        public void AddOnePointToRepair()
        {
            Repair++;
        }
        public bool CheckPeriod(DateTimeOffset[] period)
        {
            bool check = false;
            foreach (DateTimeOffset[] cartimes in ListOfCar)
            {
                if (((period[1] < cartimes[0]) ||
                    (period[0] > cartimes[1])) &&
                    (period[0] > DateTimeOffset.Now))
                {
                    check = true;
                }
            }

            return check;
        }
        public bool CheckRepair(DateTimeOffset[] time, int Repair)
        {
            bool check = true;
            if (Repair == 10)
            {
                check = false;
                GoToRepair(time);
            }
            return check;
        }
        public void GoToRepair(DateTimeOffset[] time)
        {
            DateTimeOffset[] repairDate = { DateTimeOffset.MinValue, time[1].AddDays(7) };
            Repair = 0;
        }
    }
}
