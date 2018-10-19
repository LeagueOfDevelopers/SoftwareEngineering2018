using System;

namespace SecondLesson
{
    class Program
    {
        static void Main(string[] args)
        {
			var products = new Product[]
			{
				new Product("Hleb", "Groccery", 1),
				new Product("Xiaomi", "Lowlife products", 2)
			};
			var order = new Order(products, false, false, 1);
			order.MarkArrived();
        }
    }

	public class Order
	{

		public Product[] Products { get; }
		public bool PaymentDone { get; private set; }
		public bool ShippingDone { get; private set; }
		private int _id;

		public Order(
			Product[] products,
			bool paymentDone,
			bool shippingDone, 
			int id)
		{
			Products = products;
			PaymentDone = paymentDone;
			ShippingDone = shippingDone;
			_id = id;
		}

		public void MarkArrived()
		{
			if (PaymentDone)
			{
				ShippingDone = true;
			}
		}
	}

	public class Product
	{
		public Product(string name, string category, int id)
		{
			Name = name;
			Category = category;
			Id = id;
		}

		public string Name { get; set; }
		public string Category { get; set; }
		public int Id { get; }
	} 

	public class Address
	{
		public string Street { get; }
		public string Home { get; }

	}
}
