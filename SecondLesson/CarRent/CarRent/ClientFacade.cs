using System.Collections.Generic;

namespace CarRent
{
    public class ClientFacade
    {
        public Client Client { get; }

        public ClientFacade(string name)
        {
            Client = new Client(name);
        }

        public ClientFacade(Client client)
        {
            Client = client;
        }

        public void RentCar(CarFacade carFacade, RentTime time)
        {
            Client.RentCar(carFacade, time);
        }

        public bool HasCarAt(RentTime time)
        {
            return Client.HasCarAt(time);
        }

        public Dictionary<RentTime, CarFacade> GetHistory()
        {
            return Client.RentHistory;
        }
    }
}
