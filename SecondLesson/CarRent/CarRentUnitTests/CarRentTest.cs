using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


namespace CarRent
{
    [TestClass]
    public class CarRentTest
    {
        [TestMethod]
        public void RentCarForValuableTime_AddingRecordInRentList()
        {
            User tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now) };
            var rents_list = new List<Rent> {
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 10, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 11, 23, 59, 59, TimeSpan.Zero)) };

            Cars_park park = new Cars_park(cars_list, rents_list, new List<Maintenance>(), TimeSpan.FromDays(7), 10);

            var expected_rent_list = new List<Rent> {
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 10, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 11, 23, 59, 59, TimeSpan.Zero)),
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 5, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 7, 0, 0, 0, TimeSpan.Zero))};


            park.RentCar(tenant, cars_list[0], new DateTimeOffset(2019, 5, 5, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 7, 0, 0, 0, TimeSpan.Zero));


            CollectionAssert.ReferenceEquals(park._list_of_rents, expected_rent_list);

        }
        [TestMethod]
        public void RentCarWhenItNotFree_RentListWithoutChanging()
        {
            var first_tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var second_tenant = new User("Roman", "Oganesson", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now) };
            var rents_list = new List<Rent> { new Rent(second_tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 10, 23, 59, 59, TimeSpan.Zero)) };
            var park = new Cars_park(cars_list, rents_list, new List<Maintenance>(), TimeSpan.FromDays(7), 10);

            var expected_rent_list = rents_list;
            park.RentCar(first_tenant, park.FindCarByID(car_id), new DateTimeOffset(2018, 5, 3, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2018, 5, 12, 23, 59, 59, TimeSpan.Zero));

            CollectionAssert.ReferenceEquals( expected_rent_list, park._list_of_rents);

        }

        [TestMethod]
        public void RentCarWhenUserAlreadyRentAnotherCar_RentListWithoutChanging()
        {
            User tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now), new Car(146, Guid.NewGuid(), DateTimeOffset.Now) };
            var rents_list = new List<Rent> {
                new Rent(tenant, cars_list[1], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 9, 23, 59, 59, TimeSpan.Zero))};

            Cars_park park = new Cars_park(cars_list, rents_list, new List<Maintenance>(), TimeSpan.FromDays(7), 10);

            var expected_rent_list = rents_list;
            park.RentCar(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 7, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 11, 23, 59, 59, TimeSpan.Zero));


            CollectionAssert.ReferenceEquals( expected_rent_list, park._list_of_rents);


        }

        [TestMethod]
        public void ViewFreeCarForRent_ListWithCarsThatHaveNotRentInThisPeriod()
        {
            User tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now), new Car(145, Guid.NewGuid(), DateTimeOffset.Now), new Car(189, Guid.NewGuid(), DateTimeOffset.Now) };
            var rents_list = new List<Rent> {
                new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
                new Rent(tenant, cars_list[1], new DateTimeOffset(2019, 5, 10, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 11, 23, 59, 59, TimeSpan.Zero)),
                new Rent(tenant, cars_list[2], new DateTimeOffset(2019, 5, 20, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 25, 23, 59, 59, TimeSpan.Zero)) };
            Cars_park park = new Cars_park(new List<Car>(), new List<Rent>(), new List<Maintenance>(), TimeSpan.FromDays(7), 10);

            var expected_list_of_free_cars = new List<Car> { cars_list[0], cars_list[2] };
            var list_free_cars = park.ViewListFreeCars(new DateTimeOffset(2019, 5, 10, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 12, 0, 0, 0, TimeSpan.Zero));

            CollectionAssert.ReferenceEquals( expected_list_of_free_cars, list_free_cars);
        }


        [TestMethod]
        public void ViewUserRentalHistory_ListWithCarsThatUserRented()
        {
            var tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var second_tenant = new User("Ivan", "Oganesson", Guid.NewGuid());
            Cars_park park = new Cars_park(new List<Car>(), new List<Rent>(), new List<Maintenance>(), TimeSpan.FromDays(7), 10);
            var cars_list = new List<Car> { new Car(123, Guid.NewGuid(), DateTimeOffset.Now) };
            var rents_list = new List<Rent> {
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
            new Rent(second_tenant, cars_list[0], new DateTimeOffset(2019, 5, 10, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 11, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 20, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 25, 23, 59, 59, TimeSpan.Zero)) };

            var expected_user_rent_history_list = new List<Rent> { rents_list[0], rents_list[2] };
            var tenant_rental_list = park.ViewUserRentalHistory(tenant);

            CollectionAssert.ReferenceEquals(expected_user_rent_history_list, tenant_rental_list);
        }



        [TestMethod]
        public void TryToRentCarWhileItInMaintenance_ListWithoutChange()
        {
            User tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now) };
            var rents_list = new List<Rent> { new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 2, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 3, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 5, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 6, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 7, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 8, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 11, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 12, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 13, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 14, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 15, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 16, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 17, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 18, 23, 59, 59, TimeSpan.Zero)),
            };

            Cars_park park = new Cars_park(cars_list, rents_list, new List<Maintenance>(), TimeSpan.FromDays(7), 10);
            park.RentCar(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 21, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 22, 23, 59, 59, TimeSpan.Zero));

            park.RentCar(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 23, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 24, 23, 59, 59, TimeSpan.Zero));

            var expected_rent_list = rents_list;
            expected_rent_list.Add(new Rent(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 21, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 22, 23, 59, 59, TimeSpan.Zero)));

            CollectionAssert.ReferenceEquals(park._list_of_rents, expected_rent_list);
        }
        [TestMethod]
        public void TryToRentCarBeforePlanningMaintenance_ListWithoutChange()
        {
            User tenant = new User("Ivan", "Dobry", Guid.NewGuid());
            var car_id = Guid.NewGuid();
            var cars_list = new List<Car> { new Car(123, car_id, DateTimeOffset.Now) };
            var rents_list = new List<Rent> { new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 2, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 3, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 4, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 5, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 6, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 7, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 8, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 11, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 12, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 13, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 14, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 15, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 16, 23, 59, 59, TimeSpan.Zero)),
            new Rent(tenant, cars_list[0], new DateTimeOffset(2019, 5, 17, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 18, 23, 59, 59, TimeSpan.Zero)),
            };

            Cars_park park = new Cars_park(cars_list, rents_list, new List<Maintenance>(), TimeSpan.FromDays(7), 10);
            park.RentCar(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 21, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 22, 23, 59, 59, TimeSpan.Zero));

            park.RentCar(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 19, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 20, 23, 59, 59, TimeSpan.Zero));

            var expected_rent_list = rents_list;
            expected_rent_list.Add(new Rent(tenant, park.FindCarByID(car_id), new DateTimeOffset(2019, 5, 21, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2019, 5, 22, 23, 59, 59, TimeSpan.Zero)));

            CollectionAssert.ReferenceEquals(park._list_of_rents, expected_rent_list);
        }

    }

}
