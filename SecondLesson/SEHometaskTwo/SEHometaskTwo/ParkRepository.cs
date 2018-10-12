using System.Collections.Generic;

namespace SEHometaskTwo
{
    public class ParkRepository
    {
        public ParkRepository()
        {
            AllCars = new List<Car>();
            Users = new List<User>();
        }

        public List<Car> AllCars { get; }
        public List<User> Users { get; }


        public void AddCar (Car car)
        {
            AllCars.Add(car);
        }
        public void AddUser (User user)
        {
            Users.Add(user);
        }
        public Car FindCarById(int carId)
        {
            return AllCars.Find(car => car.Id == carId);
        }
        public User FindUserById(int userId)
        {
            return Users.Find(user => user.Id == userId);
        }
        public List<Car> GetAllCars()
        {
            return AllCars;
        }
    }
}
