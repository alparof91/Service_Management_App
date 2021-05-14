using Service_Management_App;
using Service_Management_App.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Service_Management_App
{
    public class CarViewModel : BaseViewModel
    {
        #region Backing Fields
        CarDBController connection = new CarDBController();
        private List<Car> _cars = new List<Car>();
        private ObservableCollection<Car> _filteredCars = new ObservableCollection<Car>();
        private Car _selectedCar;
        private string _brand = String.Empty;
        private string _modelName = String.Empty;
        private string _modelYear = String.Empty;
        private string _color = String.Empty;
        private string _licensePlate = String.Empty;
        private string _firstName = String.Empty;
        private string _lastName = String.Empty;
        private string _iCNumber = String.Empty;
        private string _phone = String.Empty;
        private string _email = String.Empty;
        private string _filterBrand = String.Empty;
        private string _filterModel = String.Empty;
        private string _filterLicensePlate = String.Empty;
        private string _filterOwnerFirstName = String.Empty;
        #endregion

        #region Constructor
        // The constructor is private ???????????????? to enforce the factory pattern.
        public CarViewModel()
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

        public ObservableCollection<Car> FilteredCars
        {
            get { return _filteredCars; }
            set { _filteredCars = value; }
        }
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                if (value != this._selectedCar)
                {
                    this._selectedCar = value;
                    NotifyPropertyChanged();
                    DisplayDetails();
                }
            }
        }

        public string Brand
        {
            get
            {
                return this._brand;
            }

            set
            {
                if (value != this._brand)
                {
                    this._brand = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ModelName
        {
            get
            {
                return this._modelName;
            }

            set
            {
                if (value != this._modelName)
                {
                    this._modelName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ModelYear
        {
            get
            {
                return this._modelYear;
            }

            set
            {
                if (value != this._modelYear)
                {
                    this._modelYear = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Color
        {
            get
            {
                return this._color;
            }

            set
            {
                if (value != this._color)
                {
                    this._color = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string LicensePlate
        {
            get
            {
                return this._licensePlate;
            }

            set
            {
                if (value != this._licensePlate)
                {
                    this._licensePlate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get
            {
                return this._firstName;
            }

            set
            {
                if (value != this._firstName)
                {
                    this._firstName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get
            {
                return this._lastName;
            }

            set
            {
                if (value != this._lastName)
                {
                    this._lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ICNumber
        {
            get
            {
                return this._iCNumber;
            }

            set
            {
                if (value != this._iCNumber)
                {
                    this._iCNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Phone
        {
            get
            {
                return this._phone;
            }

            set
            {
                if (value != this._phone)
                {
                    this._phone = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Email
        {
            get
            {
                return this._email;
            }

            set
            {
                if (value != this._email)
                {
                    this._email = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterBrand
        {
            get
            {
                return this._filterBrand;
            }

            set
            {
                if (value != this._filterBrand)
                {
                    this._filterBrand = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterModel
        {
            get
            {
                return this._filterModel;
            }

            set
            {
                if (value != this._filterModel)
                {
                    this._filterModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterLicensePlate
        {
            get
            {
                return this._filterLicensePlate;
            }

            set
            {
                if (value != this._filterLicensePlate)
                {
                    this._filterLicensePlate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterOwnerFirstName
        {
            get
            {
                return this._filterOwnerFirstName;
            }

            set
            {
                if (value != this._filterOwnerFirstName)
                {
                    this._filterOwnerFirstName = value;
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

            int searchedCarID = connection.SearchCar(LicensePlate);
            if (searchedCarID != 0)
            {
                MessageBox.Show("This car is already in the database!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Trace.WriteLine(searchedCarID);

            Car car = new Car(1, Brand, ModelName, ParseToInt(ModelYear), Color, LicensePlate, new Owner(1, FirstName, LastName, ICNumber, Phone, Email));

            int ownerID = connection.SearchOwner(ICNumber);
            if (ownerID == 0)
            {
                int response = connection.InsertOwner(car.Owner);
                car.Owner.ID = response;
            }
            else
            {
                car.Owner.ID = ownerID;
                Trace.WriteLine(car);
            }

            int i = connection.InsertCar(car);
            if (i != 1)
            {
                MessageBox.Show("There was a problem during the insertion!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("A new car has been registered in database.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Edit()
        {
            if (!ValidateInput())
                return;
            int i = connection.UpdateCar(new Car(SelectedCar.Id, Brand, ModelName, ParseToInt(ModelYear), Color, LicensePlate, new Owner(SelectedCar.Owner.ID, FirstName, LastName, ICNumber, Phone, Email)));
            if (i == 0)
            {
                MessageBox.Show("There was a problem during the modification!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Car information updated successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Remove()
        {
            int i = connection.RemoveCar(SelectedCar);
            if (i != -1)
            {
                MessageBox.Show("There was a problem during the delete!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Car was deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Filter()
        {

            if (string.IsNullOrEmpty(FilterBrand) && string.IsNullOrEmpty(FilterModel) && string.IsNullOrEmpty(FilterLicensePlate) && string.IsNullOrEmpty(FilterOwnerFirstName))
            {
                Refresh();
                return;
            }

            _cars = connection.GetCars();
            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedCar = null;
            FilteredCars.Clear();

            List<Car> temp = new List<Car>();
            temp = FilterByBrand(_cars);
            temp = FilterByModel(temp);
            temp = FilterByLicensePlate(temp);
            temp = FilterByOwner(temp);
            temp.ForEach(x => FilteredCars.Add(x));
        }

        public void Refresh()
        {
            _cars = connection.GetCars();

            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedCar = null;
            FilteredCars.Clear();

            _cars.ForEach(x => FilteredCars.Add(x));
            ClearFields();
        }

        public void DisplayDetails()
        {
            if (SelectedCar != null)
                SetFields(SelectedCar.Brand, SelectedCar.ModelName, SelectedCar.ModelYear.ToString(), SelectedCar.Color, SelectedCar.LicensePlate, SelectedCar.Owner.FirstName, SelectedCar.Owner.LastName, SelectedCar.Owner.ICNumber, SelectedCar.Owner.Phone, SelectedCar.Owner.Email, FilterBrand, FilterModel, FilterLicensePlate, FilterOwnerFirstName);
        }
        #endregion

        #region Helper Functions
        private void ClearFields()
        {
            SetFields(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
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

        private void SetFields(string brand, string modelName, string modelYear, string color, string licensePlate, string firstName, string lastName, string iCNumber, string phone, string email, string brandFilter, string modelFilter, string licensePlateFilter, string ownerFirstNameFilter)
        {
            Brand = brand;
            ModelName = modelName;
            ModelYear = modelYear;
            Color = color;
            LicensePlate = licensePlate;
            FirstName = firstName;
            LastName = lastName;
            ICNumber = iCNumber;
            Phone = phone;
            Email = email;
            FilterBrand = brandFilter;
            FilterModel = modelFilter;
            FilterLicensePlate = licensePlateFilter;
            FilterOwnerFirstName = ownerFirstNameFilter;

        }

        private bool ValidateInput()
        {
            if (String.IsNullOrEmpty(Brand) || String.IsNullOrEmpty(ModelName) || String.IsNullOrEmpty(ModelYear) || String.IsNullOrEmpty(Color) || String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(ICNumber) || String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Rejected request!\nThere are empty fields!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (ParseToInt(ModelYear) > DateTime.Now.Year || ParseToInt(ModelYear) < 1900)
            {
                MessageBox.Show("Rejected request!\nThere was an error with the Model Date!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }


            if (isNumeric(Phone) || ICNumber.Length < 10 || ICNumber.Length > 15)
            {

                MessageBox.Show("Rejected request!\nThere was an error with the Phone Number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (isNumeric(ICNumber) || ICNumber.Length != 13)
            {
                MessageBox.Show("Rejected request!\nThere was an error with the Identity Card Number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (LicensePlate.Length > 11 || LicensePlate.Length < 8 || !LicensePlate.Equals(LicensePlate.ToUpper()))
            {
                MessageBox.Show("Rejected request!\nThere was an error with the License Plate input!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private List<Car> FilterByBrand(List<Car> input)
        {
            List<Car> output = new List<Car>();
            if (String.IsNullOrEmpty(FilterBrand))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Brand.Equals(FilterBrand))
                    output.Add(x);
            });
            return output;
        }

        private List<Car> FilterByModel(List<Car> input)
        {
            List<Car> output = new List<Car>();
            if (String.IsNullOrEmpty(FilterModel))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.ModelName.Equals(FilterModel))
                    output.Add(x);
            });
            return output;
        }

        private List<Car> FilterByLicensePlate(List<Car> input)
        {
            List<Car> output = new List<Car>();
            if (String.IsNullOrEmpty(FilterLicensePlate))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.LicensePlate.Equals(FilterLicensePlate))
                    output.Add(x);
            });
            return output;
        }

        private List<Car> FilterByOwner(List<Car> input)
        {
            List<Car> output = new List<Car>();
            if (String.IsNullOrEmpty(FilterOwnerFirstName))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Owner.FirstName.Equals(FilterOwnerFirstName))
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
