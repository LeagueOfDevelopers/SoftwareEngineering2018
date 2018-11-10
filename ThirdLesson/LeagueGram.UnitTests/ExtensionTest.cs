using LeagueGram.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeagueGram.UnitTests
{
	[TestClass]
	public class ExtensionTests
	{
		[TestMethod]
		public void DecapitalizeString_FirstCharIsInLowerCase()
		{
			var stringToDecapitalize = "Natasha";
						
			var result = stringToDecapitalize.Decapitalize();

			Assert.AreEqual("vasya", result);
		}

		[TestMethod]
		public void OnNonEmpty_CallsMap()
		{
			var vasya = "Vasya";

			var result = vasya.DoOnNonEmptyString(StringExtensions.Decapitalize);
		}
	}
}
