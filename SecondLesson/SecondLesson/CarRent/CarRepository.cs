using System;
using System.Collections.Generic;

namespace CarRent
{
	public class CarRepository
    {
		public Car[] Cars => _cars.ToArray();

		public Car GetCar(Guid carId)
		{
			return TryGetCar(carId) ?? throw new InvalidOperationException(
				$"Car with id {carId} not found");
		}

		public void SaveCar(Car car)
		{
			Car existantCar = TryGetCar(car.Id);

			if (existantCar != null)
			{
				_cars.Remove(existantCar);
			}

			_cars.Add(car);
		}

		private Car TryGetCar(Guid carId)
		{
			foreach (var car in _cars)
			{
				if (car.Id == carId)
				{
					return car;
				}
			}

			return null;
		}

		private List<Car> _cars;
    }
}
