using System;

namespace SEHometaskTwo
{
    public class User
    {
        public User(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = _id++;
            Repository = new UserRepository();

        }
        private static int _id;
        public int Id { get; }
        public string Name { get; }
        public UserRepository Repository { get; }

        public bool CanRentCar(DateTimeOffset[] desiredRentalPeriod)
        {
            var rentalHistory = Repository.GetRentalHistory();
            return rentalHistory
                   .TrueForAll(currentRent => currentRent.IsPossible(desiredRentalPeriod)); 
        }
    }
}
