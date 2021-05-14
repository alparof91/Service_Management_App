using System.Text;

namespace Service_Management_App.Data
{
    public class Owner
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ICNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Owner(int id) { ID = id; }

        public Owner(int id, string firstName, string lastName, string iCardNumber, string phone, string email)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ICNumber = iCardNumber;
            this.Phone = phone;
            this.Email = email;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Owner: ");
            sb.Append(ID).Append(", ").Append(FirstName).Append(", ").Append(LastName).Append(", ").Append(ICNumber).Append(", ").Append(Phone).Append(", ").Append(Email).Append(", ");
            return sb.ToString();
        }
    }
}
