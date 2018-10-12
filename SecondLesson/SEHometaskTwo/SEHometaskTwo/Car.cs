using System;
using System.Linq;

namespace SEHometaskTwo
{
    public class Car
    {
        public Car(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = _id++;
            Repository = new CarRepository();
        }
        private static int _id;
        public int Id { get; }
        public string Name { get; }
        public CarRepository Repository { get; }
        public int DroveWithoutRepair { get; private set; }
        public bool CanBeRented(DateTimeOffset[] desiredRentalPeriod)   
        {
            return desiredRentalPeriod[0] > DateTimeOffset.Now
                   && NotRentedIn(desiredRentalPeriod);
        }
        public bool NotRentedIn(DateTimeOffset[] desiredRentalPeriod)
        {
            var rentalHistory = Repository.GetRentalHistory();
            return rentalHistory
                  .TrueForAll(currentRent => desiredRentalPeriod[1] < currentRent[0] 
                                             || desiredRentalPeriod[0] > currentRent[1]);
        }
        public void SendForRepair()
        {
            DateTimeOffset latestRentTime = Repository.GetRentalHistory()
                                            .Select(currentPeriod => currentPeriod.Max())
                                            .Max();
            DateTimeOffset[] repairPeriod = new DateTimeOffset[] {DateTimeOffset.Now, latestRentTime};
            Repository.AddRent(repairPeriod);
            DroveWithoutRepair = 0;
        }
        public bool NeedRepair()
        {
            return DroveWithoutRepair == 10;
        }
        public void IncreaseDroveWithoutRepair()
        {
            DroveWithoutRepair++;
        }
    }
}
