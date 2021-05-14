

using Service_Management_App.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Service_Management_App
{
    public class InterventionViewModel : BaseViewModel
    {
        #region Backing Fields
        InterventionDBController connection = new InterventionDBController();
        private List<Intervention> _interventions = new List<Intervention>();
        List<Service> ServicesForIntervention;
        private ObservableCollection<Intervention> _filteredInterventions = new ObservableCollection<Intervention>();
        private Intervention _selectedIntervention;
        private string _car = String.Empty;
        private string _date = String.Empty;
        private string _status = String.Empty;
        #endregion

        #region Constructor
        // The constructor is private ???????????????? to enforce the factory pattern.
        public InterventionViewModel()
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

        public ObservableCollection<Intervention> FilteredInterventions
        {
            get { return _filteredInterventions; }
            set { _filteredInterventions = value; }
        }
        public Intervention SelectedIntervention
        {
            get { return _selectedIntervention; }
            set
            {
                if (value != this._selectedIntervention)
                {
                    this._selectedIntervention = value;
                    NotifyPropertyChanged();
                    DisplayDetails();
                }
            }
        }

        public string Car
        {
            get
            {
                return this._car;
            }

            set
            {
                if (value != this._car)
                {
                    this._car = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Date
        {
            get
            {
                return this._date;
            }

            set
            {
                if (value != this._date)
                {
                    this._date = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Status
        {
            get
            {
                return this._status;
            }

            set
            {
                if (value != this._status)
                {
                    this._status = value;
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

            //int searchedCarID = connection.SearchCar(LicensePlate);
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
            MessageBox.Show("A new car has been registered in database.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MessageBox.Show("Car information updated successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MessageBox.Show("Car was deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Filter()
        {

            //if (string.IsNullOrEmpty(FilterBrand) && string.IsNullOrEmpty(FilterModel) && string.IsNullOrEmpty(FilterLicensePlate) && string.IsNullOrEmpty(FilterOwnerFirstName))
            //{
            //    Refresh();
            //    return;
            //}

            //_interventions = connection.GetCars();
            //// before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            //// (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            //SelectedCar = null;
            //FilteredCars.Clear();

            //List<Car> temp = new List<Car>();
            //temp = FilterByBrand(_cars);
            //temp = FilterByModel(temp);
            //temp = FilterByLicensePlate(temp);
            //temp = FilterByOwner(temp);
            //temp.ForEach(x => FilteredCars.Add(x));
        }

        public void Refresh()
        {
            _interventions = connection.GetInterventions();

            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedIntervention = null;
            FilteredInterventions.Clear();

            _interventions.ForEach(x => FilteredInterventions.Add(x));
            ClearFields();
        }

        public void DisplayDetails()
        {
            ServicesForIntervention = connection.GetServiceForInterventions(SelectedIntervention);
            //if (SelectedIntervention != null)
            //    SetFields(SelectedCar.Brand, SelectedCar.ModelName, SelectedCar.ModelYear.ToString(), SelectedCar.Color, SelectedCar.LicensePlate, SelectedCar.Owner.FirstName, SelectedCar.Owner.LastName, SelectedCar.Owner.ICNumber, SelectedCar.Owner.Phone, SelectedCar.Owner.Email, FilterBrand, FilterModel, FilterLicensePlate, FilterOwnerFirstName);
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
            //Brand = brand;
            //ModelName = modelName;
            //ModelYear = modelYear;
            //Color = color;
            //LicensePlate = licensePlate;
            //FirstName = firstName;
            //LastName = lastName;
            //ICNumber = iCNumber;
            //Phone = phone;
            //Email = email;
            //FilterBrand = brandFilter;
            //FilterModel = modelFilter;
            //FilterLicensePlate = licensePlateFilter;
            //FilterOwnerFirstName = ownerFirstNameFilter;

        }

        private bool ValidateInput()
        {
            //if (String.IsNullOrEmpty(Brand) || String.IsNullOrEmpty(ModelName) || String.IsNullOrEmpty(ModelYear) || String.IsNullOrEmpty(Color) || String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(ICNumber) || String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Email))
            //{
            //    MessageBox.Show("Rejected request!\nThere are empty fields!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}

            //if (ParseToInt(ModelYear) > DateTime.Now.Year || ParseToInt(ModelYear) < 1900)
            //{
            //    MessageBox.Show("Rejected request!\nThere was an error with the Model Date!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}


            //if (isNumeric(Phone) || ICNumber.Length < 10 || ICNumber.Length > 15)
            //{

            //    MessageBox.Show("Rejected request!\nThere was an error with the Phone Number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}

            //if (isNumeric(ICNumber) || ICNumber.Length != 13)
            //{
            //    MessageBox.Show("Rejected request!\nThere was an error with the Identity Card Number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}

            //if (LicensePlate.Length > 11 || LicensePlate.Length < 8 || !LicensePlate.Equals(LicensePlate.ToUpper()))
            //{
            //    MessageBox.Show("Rejected request!\nThere was an error with the License Plate input!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}
            return true;
        }

        //private List<Car> FilterByBrand(List<Car> input)
        //{
        //    List<Car> output = new List<Car>();
        //    if (String.IsNullOrEmpty(FilterBrand))
        //    {
        //        input.ForEach(x => output.Add(x));
        //        return output;
        //    }
        //    input.ForEach(x =>
        //    {
        //        if (x.Brand.Equals(FilterBrand))
        //            output.Add(x);
        //    });
        //    return output;
        //}

        //private List<Car> FilterByModel(List<Car> input)
        //{
        //    List<Car> output = new List<Car>();
        //    if (String.IsNullOrEmpty(FilterModel))
        //    {
        //        input.ForEach(x => output.Add(x));
        //        return output;
        //    }
        //    input.ForEach(x =>
        //    {
        //        if (x.ModelName.Equals(FilterModel))
        //            output.Add(x);
        //    });
        //    return output;
        //}

        //private List<Car> FilterByLicensePlate(List<Car> input)
        //{
        //    List<Car> output = new List<Car>();
        //    if (String.IsNullOrEmpty(FilterLicensePlate))
        //    {
        //        input.ForEach(x => output.Add(x));
        //        return output;
        //    }
        //    input.ForEach(x =>
        //    {
        //        if (x.LicensePlate.Equals(FilterLicensePlate))
        //            output.Add(x);
        //    });
        //    return output;
        //}

        //private List<Car> FilterByOwner(List<Car> input)
        //{
        //    List<Car> output = new List<Car>();
        //    if (String.IsNullOrEmpty(FilterOwnerFirstName))
        //    {
        //        input.ForEach(x => output.Add(x));
        //        return output;
        //    }
        //    input.ForEach(x =>
        //    {
        //        if (x.Owner.FirstName.Equals(FilterOwnerFirstName))
        //            output.Add(x);
        //    });
        //    return output;
        //}

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
