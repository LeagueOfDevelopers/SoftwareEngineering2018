using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueGram.Infrastructure
{
	public struct Option<T>
	{
		public T Value { get; private set; }

		public bool HasValue { get; private set; }

		public static Option<T> FromValue(T value)
		{
			return new Option<T>
			{
				Value = value,
				HasValue = !Equals(value, default(T))
			};
		}
	}
}
