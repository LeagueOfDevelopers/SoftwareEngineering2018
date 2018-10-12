using System;
using System.Collections.Generic;

namespace CarRent
{
    public class RentTime
    {
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }

        public RentTime(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (startTime > endTime)
            {
                throw new Exception("Начало аренды должно быть раньше, чем её окончание");
            }

            StartTime = startTime;
            EndTime = endTime;
        }

        public bool IsCrossedWith(RentTime time)
        {
            var isCrossed = true;

            if (((StartTime > time.EndTime) && (EndTime > time.EndTime))
                || ((StartTime < time.StartTime) && (EndTime < time.EndTime)))
            {
                isCrossed = false;
            }

            return isCrossed;
        }
    }
}
