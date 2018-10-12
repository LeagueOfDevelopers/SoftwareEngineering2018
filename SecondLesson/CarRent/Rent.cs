using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public class Rent
    {
        public DateTimeOffset Start { get; }
        public DateTimeOffset Finish { get; }
        public User Tenant { get; }
        public Car CarMark { get; }

        public Rent(DateTimeOffset start, DateTimeOffset finish, User tenant, Car carmark)
        {
            Start = start;
            Finish = finish;

            if (Start > Finish)
            {
                DateTimeOffset Buf = Start;
                Start = Finish;
                Finish = Buf;
            }

            Tenant = tenant;
            CarMark = carmark;
        }

        public Rent(string start, string finish, User tenant, Car carmark)
        {
            Start = ChangeToDate(start);
            Finish = ChangeToDate(finish);

            if (Start > Finish)
            {
                DateTimeOffset Buf = Start;
                Start = Finish;
                Finish = Buf;
            }

            Tenant = tenant;
            CarMark = carmark;
        }

        private DateTimeOffset ChangeToDate(string date)
        {
            string[] dateStr = date.Split('.');
            int[] dateInt = new int[3];

            for (int i = 0; i < 3; i++)
            {
                dateInt[i] = int.Parse(dateStr[i]);
            }

            DateTimeOffset Changed = new DateTimeOffset(dateInt[2], dateInt[1], dateInt[0], 0, 0, 0, new TimeSpan(0));

            return Changed;
        }
    }
}
