using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Inspection
    {
        public Inspection(DateTimeOffset start, Car car)
        {
            Start = start;
            this.car = car;
            End = new DateTimeOffset(Start.Year, Start.Month, Start.Day + 7, 0, 0, 0,new TimeSpan());
            
        }
       
        public DateTimeOffset Start { get; }
        public DateTimeOffset End { get; }
        public Car car { get; }
    }
}
