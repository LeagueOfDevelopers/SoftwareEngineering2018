using System.Collections.Generic;

namespace SEHometaskTwo
{
    public class UserRepository
    {
        public UserRepository()
        {
            RentalHistory = new List<Rent>();
        }

        public List<Rent> RentalHistory { get; }
        public void AddRent(Rent rent)
        {
            RentalHistory.Add(rent);
        }
        public List<Rent> GetRentalHistory()
        {
            return RentalHistory;
        }
    }
}
