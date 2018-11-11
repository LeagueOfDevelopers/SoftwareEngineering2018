using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class RentContract
    {
        public RentContract(DateTimeOffset startRent, DateTimeOffset endRent, User user, Car car)
        {
            StartRent = startRent;
            EndRent = endRent;
            User = user ?? throw new ArgumentNullException(nameof(user));
            this.car = car ;
        }
        public DateTimeOffset StartRent { get; }
        public DateTimeOffset EndRent { get; }
        public User User { get;  }
        public Car car { get; }
    }
}
