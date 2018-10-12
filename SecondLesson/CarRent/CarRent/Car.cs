using System;


namespace CarRent
{
    public class Car
    {
        public string Name { get; private set; }
        public Guid Id { get; }

        public Car(string name)
        {
            this.Name = name;
            this.Id = new Guid();
        }

        public override string ToString()
        {
            return Name + " " + Id.ToString();
        }

    }
}
