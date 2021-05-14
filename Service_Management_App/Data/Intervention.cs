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
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }
        public int Paid { get; set; }
        public bool IsActive { get; set; }

        public Intervention(int id, Car car, Employee employee, DateTime date, string status, int paid, bool isActive)
        {
            Id = id;
            Car = car;
            Employee = employee;
            Date = date;
            Status = status;
            Paid = paid;
            IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Intervention: ");
            sb.Append(Car.LicensePlate).Append(", ").Append(Date.ToString()).Append(", ").Append(Status).Append(", ").Append(Paid).Append(", ").Append(IsActive);
            return sb.ToString();
        }
    }
}
