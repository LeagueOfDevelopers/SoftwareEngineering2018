using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueGram.Infrastructure
{
	public static class StringExtensions
	{
		public static string Decapitalize(this string input)
		{
			var firstChar = input[0].ToString().ToLower();
			return firstChar + input.Substring(1);
		}

		public static T DoOnNonEmptyString<T>(this string input, Func<string, T> map)
		{
			if (string.IsNullOrEmpty(input))
			{
				return default(T);
			}

			return map(input);
		}

		public static IEnumerable<T> Times<T>(this int input, Func<int, T> generate)
		{
			var arr = new T[input];
			for (int i = 0; i < input; i++)
			{
				arr[i] = generate(i);
			}

			return arr;
		}
	}
}
