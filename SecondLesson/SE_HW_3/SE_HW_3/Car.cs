using System;

namespace SE_HW_3
{
    public class Car
    {
        public string Model { get; }
        public Guid Id { get; }
        public CarStatus Status { get; private set; }

        public int RentCount;
        public int ServiceCount;

        public DateTimeOffset RentStart;
        public TimeSpan RentDuration;


        public Car(string model)
        {
            Model = model;
            Id = new Guid();
            Status = CarStatus.Free;
            RentCount = 0;
            ServiceCount = 0;
            RentStart = DateTimeOffset.Now;
            RentDuration = TimeSpan.Zero;
        }


        public Car(string model, Guid id, CarStatus status, int rentCount, int serviceCount, DateTimeOffset rentStart, TimeSpan rentDuration)
        {
            Model = model;
            Id = id;
            Status = status;
            RentCount = rentCount;
            ServiceCount = serviceCount;
            RentStart = rentStart;
            RentDuration = rentDuration;
        }


        public void ChangeStatus(Car car, CarStatus status)
        {
            car.Status = status;
        }
    }
}
