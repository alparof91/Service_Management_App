using System;
using System.Text;

namespace Service_Management_App.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public Owner Owner { get; set; }

        public Car(int id, string brand, string modelName, int modelYear, string color, string licensePlate, Owner owner)
        {
            this.Id = id;
            this.Brand = brand;
            this.ModelName = modelName;
            this.ModelYear = modelYear;
            this.Color = color;
            this.LicensePlate = licensePlate;
            this.Owner = owner;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Car: ");
            sb.Append(Id).Append(", ").Append(Brand).Append(", ").Append(ModelName).Append(", ").Append(ModelYear).Append(", ").Append(Color).Append(", ").Append(LicensePlate).Append(", ").Append(Owner);
            return sb.ToString();
        }
    }
}
