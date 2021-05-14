using Service_Management_App.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Service_Management_App
{
    public class ServicesViewModel : BaseViewModel
    {
        #region Backing Fields
        ServiceDBController connection = new ServiceDBController();
        private List<Service> _services = new List<Service>();
        private ObservableCollection<Service> _filteredServices = new ObservableCollection<Service>();
        private Service _selectedService;
        private string _type = String.Empty;
        private string _name = String.Empty;
        private string _time = String.Empty;
        private string _price = String.Empty;
        private string _filterType = String.Empty;
        private string _filterName = String.Empty;
        private string _filterTime = String.Empty;
        private string _filterPrice = String.Empty;
        #endregion

        #region Constructor
        // The constructor is private ???????????????? to enforce the factory pattern.
        public ServicesViewModel()
        {
            this.SubmitCommand = new RelayCommand(Submit);
            this.EditCommand = new RelayCommand(Edit);
            this.RemoveCommand = new RelayCommand(Remove);
            this.ClearFieldsCommand = new RelayCommand(ClearFields);
            this.RefreshCommand = new RelayCommand(Refresh);
            this.FilterCommand = new RelayCommand(Filter);
            Refresh();
        }
        #endregion

        #region Properties
        public ICommand SubmitCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        public ObservableCollection<Service> FilteredServices
        {
            get { return _filteredServices; }
            set { _filteredServices = value; }
        }
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                if (value != this._selectedService)
                {
                    this._selectedService = value;
                    NotifyPropertyChanged();
                    DisplayDetails();
                }
            }
        }

        public string Type
        {
            get
            {
                return this._type;
            }

            set
            {
                if (value != this._type)
                {
                    this._type = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Time
        {
            get
            {
                return this._time;
            }

            set
            {
                if (value != this._time)
                {
                    this._time = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Price
        {
            get
            {
                return this._price;
            }

            set
            {
                if (value != this._price)
                {
                    this._price = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterType
        {
            get
            {
                return this._filterType;
            }

            set
            {
                if (value != this._filterType)
                {
                    this._filterType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterName
        {
            get
            {
                return this._filterName;
            }

            set
            {
                if (value != this._filterName)
                {
                    this._filterName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterTime
        {
            get
            {
                return this._filterTime;
            }

            set
            {
                if (value != this._filterTime)
                {
                    this._filterTime = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterPrice
        {
            get
            {
                return this._filterPrice;
            }

            set
            {
                if (value != this._filterPrice)
                {
                    this._filterPrice = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public void Submit()
        {
            if (!ValidateInput())
                return;

            //int searchedServiceID = connection.SearchService(LicensePlate);
            //if (searchedCarID != 0)
            //{
            //    MessageBox.Show("This car is already in the database!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //Trace.WriteLine(searchedCarID);

            //Car car = new Car(1, Brand, ModelName, ParseToInt(ModelYear), Color, LicensePlate, new Owner(1, FirstName, LastName, ICNumber, Phone, Email));

            //int ownerID = connection.SearchOwner(ICNumber);
            //if (ownerID == 0)
            //{
            //    int response = connection.InsertOwner(car.Owner);
            //    car.Owner.ID = response;
            //}
            //else
            //{
            //    car.Owner.ID = ownerID;
            //    Trace.WriteLine(car);
            //}

            //int i = connection.InsertCar(car);
            //if (i != 1)
            //{
            //    MessageBox.Show("There was a problem during the insertion!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //MessageBox.Show("A new car has been registered in database.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Edit()
        {
            if (!ValidateInput())
                return;
            //int i = connection.UpdateCar(new Car(SelectedCar.Id, Brand, ModelName, ParseToInt(ModelYear), Color, LicensePlate, new Owner(SelectedCar.Owner.ID, FirstName, LastName, ICNumber, Phone, Email)));
            //if (i == 0)
            //{
            //    MessageBox.Show("There was a problem during the modification!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //MessageBox.Show("Car information updated successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Remove()
        {
            //int i = connection.RemoveCar(SelectedCar);
            //if (i != -1)
            //{
            //    MessageBox.Show("There was a problem during the delete!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //MessageBox.Show("Car was deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Filter()
        {

            if (string.IsNullOrEmpty(FilterType) && string.IsNullOrEmpty(FilterName) && string.IsNullOrEmpty(FilterTime) && string.IsNullOrEmpty(FilterPrice))
            {
                Refresh();
                return;
            }

            _services = connection.GetServices();
            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedService = null;
            FilteredServices.Clear();

            List<Service> temp = new List<Service>();
            temp = FilterByType(_services);
            temp = FilterByName(temp);
            temp = FilterByTime(temp);
            temp = FilterByPrice(temp);
            temp.ForEach(x => FilteredServices.Add(x));
        }

        public void Refresh()
        {
            _services = connection.GetServices();

            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedService = null;
            FilteredServices.Clear();

            _services.ForEach(x => FilteredServices.Add(x));
            ClearFields();
        }

        public void DisplayDetails()
        {
            if (SelectedService != null)
                SetFields(SelectedService.Type.Name, SelectedService.Name, SelectedService.Time.ToString(), SelectedService.Price.ToString(), String.Empty, String.Empty, String.Empty, String.Empty);
        }

        #endregion

        #region Helper Functions
        private void ClearFields()
        {
            SetFields(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        }

        private int ParseToInt(string input)
        {
            try
            {
                return Int32.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input}'");
                return 0;
            }
        }

        private void SetFields(string type, string name, string time, string price, string typeFilter, string nameFilter, string timeFilter, string priceFilter)
        {
            Type = type;
            Name = name;
            Time = time;
            Price = price;
            FilterType = typeFilter;
            FilterName = nameFilter;
            FilterTime = timeFilter;
            FilterPrice = priceFilter;

        }

        private bool ValidateInput()
        {
            if (String.IsNullOrEmpty(Type) || String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Time) || String.IsNullOrEmpty(Price))
            {
                MessageBox.Show("Rejected request!\nThere are empty fields!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //if (ParseToInt(Type) > DateTime.Now.Year || ParseToInt(ModelYear) < 1900)
            //{
            //    MessageBox.Show("Rejected request!\nThere was an error with the Model Date!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}


            //if (isNumeric(Phone) || ICNumber.Length < 10 || ICNumber.Length > 15)
            //{

            //    MessageBox.Show("Rejected request!\nThere was an error with the Phone Number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}

            if (isNumeric(Time))
            {
                MessageBox.Show("Rejected request!\nThere was an error with the Estimated Time input!\nIt has to be numeric!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (!Decimal.TryParse(Price, out _))
            {
                MessageBox.Show("Rejected request!\nThere was an error with the Price input!\nIt has to be decimal!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private List<Service> FilterByType(List<Service> input)
        {
            List<Service> output = new List<Service>();
            if (String.IsNullOrEmpty(FilterType))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Type.Name.Equals(FilterType))
                    output.Add(x);
            });
            return output;
        }

        private List<Service> FilterByName(List<Service> input)
        {
            List<Service> output = new List<Service>();
            if (String.IsNullOrEmpty(FilterName))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Name.Equals(FilterName))
                    output.Add(x);
            });
            return output;
        }

        private List<Service> FilterByTime(List<Service> input)
        {
            List<Service> output = new List<Service>();
            if (String.IsNullOrEmpty(FilterTime))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Time.Equals(Int32.Parse(FilterTime)))
                    output.Add(x);
            });
            return output;
        }

        private List<Service> FilterByPrice(List<Service> input)
        {
            List<Service> output = new List<Service>();
            if (String.IsNullOrEmpty(FilterPrice))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Price.Equals(Decimal.Parse(FilterPrice)))
                    output.Add(x);
            });
            return output;
        }

        private bool isNumeric(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int nr = (int)i;
                if (i < 48 || i > 57)
                    return false;
            }
            return true;
        }

        #endregion

        public override string ToString()
        {
            //return base.ToString();
            return "Hello";
        }
    }
}
