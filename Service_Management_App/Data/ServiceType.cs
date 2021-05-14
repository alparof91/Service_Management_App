using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Management_App.Data
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ServiceType(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Service Type: ");
            sb.Append(Id).Append(", ").Append(Name).Append(", ").Append(IsActive);
            return sb.ToString();
        }
    }
}
