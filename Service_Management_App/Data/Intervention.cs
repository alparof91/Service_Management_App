using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Management_App.Data
{
    public class Intervention
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }
        public bool IsActive { get; set; }

        public Intervention(int id, Car car, DateTime date, string status, bool isActive)
        {
            Id = id;
            Car = car;
            Date = date;
            Status = status;
            IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Intervention: ");
            sb.Append(Id).Append(", ").Append(Car.LicensePlate).Append(", ").Append(Date.ToString()).Append(", ").Append(Status).Append(IsActive);
            return sb.ToString();
        }
    }
}
