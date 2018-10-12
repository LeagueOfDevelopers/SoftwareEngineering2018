using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarRentApp
{
    public class User
    {
        public User(string name, List<DateTimeOffset[]> listOfUser)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ListOfUser = listOfUser ?? throw new ArgumentNullException(nameof(listOfUser));
            HistoryOfUser = new List<RentOfUser>();
            Id = _id++;
        }
        private static int _id;
        public int Id { get; private set; }
        string Name { get; }
        public List<DateTimeOffset[]> ListOfUser { get; set; }
        public List<RentOfUser> HistoryOfUser { get; }
        public bool CheckPeriod(DateTimeOffset[] period)
        {
            bool check = false;
            foreach (DateTimeOffset[] usertimes in ListOfUser)
            {
                if (((period[0]>usertimes[1]) ||
                    (period[1]<usertimes[0]))&&
                    (period[0] > DateTimeOffset.Now))
                {
                    check = true;
                }
            }

            return check;
        }
        public List<RentOfUser> GetHistoryOfUserRent()
        {
            return HistoryOfUser;
        }
    }
}
