using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecondLesson;

namespace SecondLessonTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void TestOrder()
        {
			var order = new Order(new Product[0], false, false, 1);

			order.MarkArrived();

			Assert.IsTrue(order.ShippingDone);
        }

		[TestMethod]
		public void TestPrimitive()
		{
			var number = 15;
			var expected = 16;
			var anotherNumber = number;

			number++;

			Assert.AreEqual(anotherNumber, number);
		}

		private void ChangeOrder(Order order)
		{
			order.MarkPaid();
		}

		private void ChangePrimitive(int number)
		{
			number++;
		}
    }
}
