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


        public void AddCar(string model){
            RentCompany.AddCar(model);
        }


        public void SendToService(Car car)
        {
            RentCompany.SendToService(car);
        }
    }
}
