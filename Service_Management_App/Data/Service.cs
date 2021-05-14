
using System.Text;

namespace Service_Management_App.Data
{
    public class Service
    {
        public int Id { get; set; }
        public ServiceType Type { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public Service(int id, ServiceType type, string name, int time, decimal price, bool isActive)
        {
            this.Id = id;
            this.Type = type;
            this.Name = name;
            this.Time = time;
            this.Price = price;
            this.IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Service: ");
            sb.Append(Id).Append(", ").Append(Type.Name).Append(", ").Append(Name).Append(", ").Append(Price).Append(", ").Append(IsActive);
            return sb.ToString();
        }
    }
}
