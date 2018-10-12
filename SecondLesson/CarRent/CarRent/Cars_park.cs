using System;
using System.Collections.Generic;


namespace CarRent
{
    public class Cars_park
    {
        private List<Car> _list;
        public List<Rent> _list_of_rents { private set; get; }
        private List<Maintenance> _list_of_time_maintensnce;
        private TimeSpan _maintenance_duration;
        private int _amount_of_rent_without_maintenance;

        public Cars_park(List<Car> list, List<Rent> list_of_rents, List<Maintenance> list_of_time_maintensnce, TimeSpan maintenance_duration, int amount_of_rent_without_maintenance)
        {
            _list = list;
            _list_of_rents = list_of_rents;
            _list_of_time_maintensnce = list_of_time_maintensnce;
            _maintenance_duration = maintenance_duration;
            _amount_of_rent_without_maintenance = amount_of_rent_without_maintenance;
        }

        public Car FindCarByID(Guid id)
        {
            foreach (Car car in _list)
            {
                if(car._id == id)
                {
                    return car;
                }
            }
            return null;
        }
        public void AddNewCarInCarPark(Car adding_car)
        {
            _list.Add(adding_car);
        }

        public void RentCar(User tenant, Car rental_car, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            if (CheckIsCarFree(rental_car, date_of_begining.Date, date_of_ending.Date) && CheckIsTenantFree(tenant, date_of_begining.Date, date_of_ending.Date)&&(rental_car._end_date_of_last_maintenance.Date<=date_of_begining.Date))
            {                
                _list_of_rents.Add(new Rent(tenant, rental_car, date_of_begining, date_of_ending)); 
                if (CheckItIsTimeRorMaintenance(rental_car))
                {
                    SendCarForMaintenance(rental_car, GetEndDateOfLestRent(rental_car));
                }
            }
            else Print("Error");

        }

        private DateTimeOffset GetEndDateOfLestRent(Car rental_car)
        {
            DateTimeOffset end_date_of_last_rent = DateTimeOffset.MinValue;
            foreach (Rent rent in _list_of_rents)
            {
                if ((rent._car == rental_car) && (rent._begining_date.Date >= rental_car._end_date_of_last_maintenance.Date))
                {                    
                    if (rent._ending_date > end_date_of_last_rent)
                    {
                        end_date_of_last_rent = rent._ending_date;
                    }
                }
            }
            return end_date_of_last_rent;

        }
        private int GetAmountOfRentAfterLastMaintenance(Car rental_car)
        {
            int this_car_amount_of_rent = 0;

            foreach (Rent rent in _list_of_rents)
            {
                if ((rent._car == rental_car) && (rent._begining_date.Date >= rental_car._end_date_of_last_maintenance.Date))
                {
                    this_car_amount_of_rent++;
                }
            }
            return this_car_amount_of_rent;
        }
        private bool CheckItIsTimeRorMaintenance(Car rental_car)
        {            
            if (_amount_of_rent_without_maintenance == GetAmountOfRentAfterLastMaintenance(rental_car))
            {                               
                return true;
            }
            return false;
        }
        private void SendCarForMaintenance(Car rental_car, DateTimeOffset end_date_of_last_rent)
        {
            _list_of_time_maintensnce.Add(new Maintenance(rental_car, end_date_of_last_rent, end_date_of_last_rent + _maintenance_duration));
            rental_car._end_date_of_last_maintenance = end_date_of_last_rent + _maintenance_duration;
        }

        public List<Car> ViewListFreeCars(DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            List<Car> freecars = new List<Car>();
            foreach (Rent i in _list_of_rents)
            {
                if (CheckIsCarFree(i._car, date_of_begining, date_of_ending)&&(i._car._end_date_of_last_maintenance.Date<=date_of_begining.Date))
                {
                    freecars.Add(i._car);
                }
            }
            return freecars;
        }
        public List<Rent> ViewUserRentalHistory(User tenant)
        {
            List<Rent> rent_list = new List<Rent>();
            foreach (Rent i in _list_of_rents)
            {
                if (i._tenant == tenant)
                {
                    rent_list.Add(i);
                }
            }
            return rent_list;
        }

        private bool CheckIsTenantFree(User tenant, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            foreach (Rent i in _list_of_rents)
            {
                if ((i._tenant == tenant) && !(date_of_begining.Date > i._ending_date.Date || date_of_ending.Date < i._begining_date.Date))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckIsCarFree(Car rental_car, DateTimeOffset date_of_begining, DateTimeOffset date_of_ending)
        {
            foreach (Rent i in _list_of_rents)
            {
                if ((i._car == rental_car) && !(date_of_begining.Date > i._ending_date.Date || date_of_ending.Date < i._begining_date.Date))
                {
                    return false;
                }
            }
            return true;
        }

        public void Print(string text)
        {
            Console.WriteLine(text);
        }




    }
}
