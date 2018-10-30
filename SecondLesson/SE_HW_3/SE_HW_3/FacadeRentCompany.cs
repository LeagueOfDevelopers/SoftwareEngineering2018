using System;
using System.Collections.Generic;

namespace SE_HW_3
{
    public class FacadeRentCompany
    {
        public RentCompany RentCompany;


        public FacadeRentCompany(RentCompany rentCompany)
        {
            RentCompany = rentCompany;
        }


        public List<Car> GetAvailableCars()
        {
            return CarShower.GetAvailableCars(RentCompany.AllCars);
        }


        public void AddCar(Car car){
            RentCompany.AddCar(car);
        }


        public void SendToService(Car car)
        {
            RentCompany.SendToService(car);
        }
    }
}
