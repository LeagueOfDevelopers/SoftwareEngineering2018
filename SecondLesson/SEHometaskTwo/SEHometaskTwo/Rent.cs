using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEHometaskTwo
{
    public class Rent
    {
        public Rent(Car car, DateTimeOffset[] rentalPeriod)
        {
            RentalPeriod = rentalPeriod ?? throw new ArgumentNullException(nameof(rentalPeriod));
            Car = car ?? throw new ArgumentNullException(nameof(car));
        }

        public DateTimeOffset[] RentalPeriod { get; }
        public Car Car { get; }
        public bool IsPossible(DateTimeOffset[] desiredRentalPeriod)
        {
            return desiredRentalPeriod[1] < RentalPeriod[0] || desiredRentalPeriod[0] > RentalPeriod[1]; 
        }
    }
}
