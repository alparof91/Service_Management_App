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
    public class EmployeeViewModel : BaseViewModel
    {
        #region Backing Fields
        EmployeeDBController connection = new EmployeeDBController();
        private List<Employee> _employees = new List<Employee>();
        private ObservableCollection<Employee> _filteredEmployees = new ObservableCollection<Employee>();
        private Employee _selectedEmployee;
        private int _id;
        private string _firstName;
        private string _lastName = String.Empty;
        private string _iCNumber = String.Empty;
        private string _hiringDate = String.Empty;
        private string _phone = String.Empty;
        private string _email = String.Empty;
        private bool _isActive = true;
        private string _filterFirstName = String.Empty;
        private string _filterLastName = String.Empty;
        private string _filterICNumber = String.Empty;
        private string _filterPhone = String.Empty;
        #endregion

        #region Constructor
        // The constructor is private ???????????????? to enforce the factory pattern.
        public EmployeeViewModel()
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

        public ObservableCollection<Employee> FilteredEmployees
        {
            get { return _filteredEmployees; }
            set { _filteredEmployees = value; }
        }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (value != this._selectedEmployee)
                {
                    this._selectedEmployee = value;
                    NotifyPropertyChanged();
                    DisplayDetails();
                }
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }

            set
            {
                if (value != this._id)
                {
                    this._id = value;
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
        public string HiringDate
        {
            get
            {
                return this._hiringDate;
            }

            set
            {
                if (value != this._hiringDate)
                {
                    this._hiringDate = value;
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
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }

            set
            {
                if (value != this._isActive)
                {
                    this._isActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FilterFirstName
        {
            get
            {
                return this._filterFirstName;
            }

            set
            {
                if (value != this._filterFirstName)
                {
                    this._filterFirstName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterLastName
        {
            get
            {
                return this._filterLastName;
            }

            set
            {
                if (value != this._filterLastName)
                {
                    this._filterLastName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterICNumber
        {
            get
            {
                return this._filterICNumber;
            }

            set
            {
                if (value != this._filterICNumber)
                {
                    this._filterICNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string FilterPhone
        {
            get
            {
                return this._filterPhone;
            }

            set
            {
                if (value != this._filterPhone)
                {
                    this._filterPhone = value;
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

            Trace.WriteLine(ParseToDate(HiringDate).ToString());

            if (connection.SearchEmployee(ICNumber) != 0)
            {
                MessageBox.Show("Employee with this Identity Card Number is already in database.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int i = connection.InsertEmployee(new Employee(1, FirstName, LastName, ICNumber, DateTime.Parse(HiringDate), Phone, Email, true));
            if (i == 0)
            {
                MessageBox.Show("There was a problem during the insertion!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("A new employee has been registered in database.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Edit()
        {
            if (!ValidateInput())
                return;

            int i = connection.UpdateEmployee(new Employee(SelectedEmployee.Id, FirstName, LastName, ICNumber, DateTime.Parse(HiringDate), Phone, Email, true));
            if (i != -1)
            {
                MessageBox.Show("There was a problem during the modification!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Employee information updated successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Remove()
        {
            int i = connection.RemoveEmployee(SelectedEmployee);
            if (i != -1)
            {
                MessageBox.Show("There was a problem during the delete!\nPlease contact the support team!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Employee was deleted!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }

        public void Filter()
        {

            if (string.IsNullOrEmpty(FilterFirstName) && string.IsNullOrEmpty(FilterLastName) && string.IsNullOrEmpty(FilterICNumber) && string.IsNullOrEmpty(FilterPhone))
            {
                Refresh();
                return;
            }

            _employees = connection.GetEmployees();
            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedEmployee = null;
            FilteredEmployees.Clear();

            List<Employee> temp = new List<Employee>();
            temp = FilterByFirstName(_employees);
            temp = Utils.FilterByLastName(temp, FilterLastName);
            temp = FilterByICNumber(temp);
            temp = FilterByPhone(temp);
            temp.ForEach(x => FilteredEmployees.Add(x));
        }

        public void Refresh()
        {
            _employees = connection.GetEmployees();

            // before  FilteredCars.Clear() the SelectedCar needs to point to a valid location
            // (with  FilteredCars.Clear(), the referenced object will disappear, and will give error at Display Fields)
            SelectedEmployee = null;
            FilteredEmployees.Clear();

            _employees.ForEach(x => FilteredEmployees.Add(x));
            ClearFields();
        }

        public void DisplayDetails()
        {
            if (SelectedEmployee != null)
                SetFields(SelectedEmployee.FirstName, SelectedEmployee.LastName, SelectedEmployee.ICNumber, SelectedEmployee.HiringDate.Date.ToString(), SelectedEmployee.Phone, SelectedEmployee.Email, FilterFirstName, FilterLastName, FilterICNumber, FilterPhone);
            //SelectedEmployee.HiringDate.ToString()
        }
        #endregion

        #region Helper Functions
        private void ClearFields()
        {
            SetFields(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
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

        private DateTime ParseToDate(string input)
        {
            try
            {
                return DateTime.Parse(input);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input}'");
                return DateTime.MinValue;
            }
        }

        private void SetFields(string firstName, string lastName, string iCNumber, string hiringDate, string phone, string email, string firstNameFilter, string lastNameFilter, string iCNumberFilter, string phoneFilter)
        {
            FirstName = firstName;
            LastName = lastName;
            ICNumber = iCNumber;
            HiringDate = hiringDate;
            Phone = phone;
            Email = email;
            FilterFirstName = firstNameFilter;
            FilterLastName = lastNameFilter;
            FilterICNumber = iCNumberFilter;
            FilterPhone = phoneFilter;

        }

        private bool ValidateInput()
        {
            if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(ICNumber) || HiringDate.Equals(DateTime.Today) || String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Rejected request!\nThere are empty fields!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (ParseToDate(HiringDate) == DateTime.MinValue)
            {
                MessageBox.Show("Rejected request!\nThere was an error with the Hiring Date input! Please check the format!", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }



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

            return true;
        }

        private List<Employee> FilterByFirstName(List<Employee> input)
        {
            List<Employee> output = new List<Employee>();
            if (String.IsNullOrEmpty(FilterFirstName))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.FirstName.Equals(FilterFirstName))
                    output.Add(x);
            });
            return output;
        }

        private List<Employee> FilterByICNumber(List<Employee> input)
        {
            List<Employee> output = new List<Employee>();
            if (String.IsNullOrEmpty(FilterICNumber))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.ICNumber.Equals(FilterICNumber))
                    output.Add(x);
            });
            return output;
        }

        private List<Employee> FilterByPhone(List<Employee> input)
        {
            List<Employee> output = new List<Employee>();
            if (String.IsNullOrEmpty(FilterPhone))
            {
                input.ForEach(x => output.Add(x));
                return output;
            }
            input.ForEach(x =>
            {
                if (x.Phone.Equals(FilterPhone))
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
