using System;
using System.Collections.Generic;

namespace Service_Management_App.Data
{
    class Appointment
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Car Car { get; set; }
        public Employee Employee { get; set; }
        public Service Service { get; set; }

        public Appointment(int id, DateTime date, Car car, Employee employee, Service Service)
        {
            this.ID = id;
            this.Date = date;
            this.Car = car;
            this.Employee = employee;
            this.Service = Service;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
