using Service_Management_App.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Management_App
{
    public class FilterUtils
    {
        public static List<Employee> FilterByLastName(List<Employee> input, string lastName)
        {
            List<Employee> output = new List<Employee>();
            if (String.IsNullOrEmpty(lastName))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.LastName.Equals(lastName))
                    output.Add(x);
            });
            return output;
        }
    }
}
