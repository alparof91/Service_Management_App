using Service_Management_App.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Service_Management_App
{
    public class Utils
    {
        public static void listToObservable<T>(ObservableCollection<T> observableList, List<T> list)
        {
            observableList.Clear();
            list.ForEach(x => observableList.Add(x));
        }

        public static void checkboxFilter(ObservableCollection<SelectableService> outputObsList, ObservableCollection<Service> intputObsList, List<Service> list)
        {
            outputObsList.Clear();
            list.ForEach(x =>
            {
                if (intputObsList.Any(y => y.Name == x.Name))
                    outputObsList.Add(new SelectableService(true, x));
                else
                    outputObsList.Add(new SelectableService(false, x));
            });
        }

        public static List<Employee> FilterByLastName(List<Employee> input, string lastName)
        {
            List<Employee> output = new List<Employee>();
            if (string.IsNullOrEmpty(lastName))
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
