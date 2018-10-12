using System;
using System.Collections.Generic;

namespace SEHometaskTwo
{
    public class CarRepository
    {
        public CarRepository()
        {
            RentalHistory = new List<DateTimeOffset[]>();
        }

        public List<DateTimeOffset[]> RentalHistory { get; }
        public void AddRent(DateTimeOffset[] rentalPeriod)
        {
            RentalHistory.Add(rentalPeriod);
        }
        public List<DateTimeOffset[]> GetRentalHistory()
        {
            return RentalHistory;
        }
    }
}
