using System;
using System.Text;

namespace Service_Management_App.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ICNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public Employee(string firstName)
        {
            this.FirstName = firstName;
        }

        public Employee(int id, string firstName, string lastName, string iCNumber, DateTime hiringDate, string phone, string email, bool isActive)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ICNumber = iCNumber;
            this.HiringDate = hiringDate;
            this.Phone = phone;
            this.Email = email;
            this.IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Employee: ");
            sb.Append(FirstName).Append(", ").Append(LastName).Append(", ").Append(ICNumber).Append(", ").Append(HiringDate).Append(", ").Append(Phone).Append(", ").Append(Email).Append(", ").Append(IsActive);
            return sb.ToString();
        }
    }
}
