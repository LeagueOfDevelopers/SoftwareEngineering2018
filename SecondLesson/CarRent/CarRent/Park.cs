using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Park
    {
        public Park(List<Car> cars, List<Inspection> insp)
        {
            Cars = cars ?? throw new ArgumentNullException(nameof(cars));
            Insp = insp ?? throw new ArgumentNullException(nameof(insp));
        }

        public List<Car> Cars { get; private set; }
        public List<Inspection> Insp { get; private set; }

        public void AddCar(Car car)
        {
            if (!Cars.Contains(car))
                Cars.Add(car);
        }

        public List<Car> SeeCarsOnInspection(DateTimeOffset start, DateTimeOffset end)
        {
            List<Car> Cars = new List<Car>();
            foreach (Inspection insp in Insp)
            {
                if ((start > insp.Start) && (end < insp.End)) Cars.Add(insp.car);
            }
            return Cars;
        }

        public void GoOnInspection(Car car, DateTimeOffset start)
        {
            Insp.Add(new Inspection(start, car));
        }
       
    }
}
