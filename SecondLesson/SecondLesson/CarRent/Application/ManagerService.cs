﻿using System;
using System.Collections.Generic;

namespace CarRent.Application
{
	public class ManagerService
    {
		public ManagerService(
			CarRepository carRepository,
			ClientRepository clientRepository,
			int rentsAmountBeforeMaintenance, 
			int maintenancePeriodInDays)
		{
			_carRepository = carRepository;
			_clientRepository = clientRepository;
			_rentsAmountBeforeMaintenance = rentsAmountBeforeMaintenance;
			_maintenancePeriodInDays = maintenancePeriodInDays;
		}

		public Guid AddCar(string name, string color)
		{
			var car = new Car(
				Guid.NewGuid(), 
				name, color, 
				Array.Empty<DatePeriod>(), 
				Array.Empty<Rent>(),
				_rentsAmountBeforeMaintenance,
				_maintenancePeriodInDays);
			_carRepository.SaveCar(car);
			return car.Id;
		} 

		public Guid RegisterClient(string name)
		{
			var client = new Client(Guid.NewGuid(), name, Array.Empty<Rent>());
			_clientRepository.SaveClient(client);
			return client.Id;
		}

		private readonly CarRepository _carRepository;
		private readonly ClientRepository _clientRepository;
		private readonly int _rentsAmountBeforeMaintenance;
		private readonly int _maintenancePeriodInDays;
	}
}
