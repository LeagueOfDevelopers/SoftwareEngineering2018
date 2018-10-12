using System.Collections.Generic;

namespace CarRent
{
    public class CarRepository : IRepository<Car>
    {
        private List<Car> Cars { get; set; }

        public CarRepository()
        {
            Cars = new List<Car>();
        }

        public CarRepository(List<Car> cars)
        {
            Cars = cars;
        }

        public void Create(Car car)
        {
            Cars.Add(car);
        }

        public void Delete(Car car)
        {
            Cars.Remove(car);
        }

        public Car GetItem(string name)
        {
            return Cars.Find(x => x.Name == name);
        }

        public Car GetItem(int position)
        {
            return Cars.ToArray()[position];
        }

        public IEnumerable<Car> GetList()
        {
            return Cars;
        }

        public void Update(Car car)
        {
            Cars.RemoveAll(x => x.Name == car.Name);
            Cars.Add(car);
        }

        public int GetSize()
        {
            return Cars.Count;
        }
    }
}
