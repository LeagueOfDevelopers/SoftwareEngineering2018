using System;

namespace CarRent
{
	public class DatePeriod
    {
		public DatePeriod(DateTimeOffset start, DateTimeOffset end)
		{
			Start = start.Date;
			End = end.Date;
			if (Start > End)
			{
				throw new ArgumentException("Start date should not be later than end date");
			}
		}

		public DateTimeOffset Start { get; }
		public DateTimeOffset End { get; }

		public bool IntersectsWith(DatePeriod anotherPeriod)
		{
			return !(Start > anotherPeriod.End || End < anotherPeriod.Start);
		}
    }
}
