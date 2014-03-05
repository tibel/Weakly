using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Weakly.MVVM;

namespace Demo.Validation
{
    public class MainViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly Company _company;

        public MainViewModel(Company company)
        {
            _validation.Validators.Add(new DataAnnotationsValidator(GetType()));
            _company = company;
            SaveCommand = WeakCommand.Create(Save);
        }

        [Required(ErrorMessage = @"Name is required.")]
        public string CName
        {
            get { return _company.Name; }
            set
            {
                _company.Name = value;
                OnPropertyChanged(value);
            }
        }

        [Required(ErrorMessage = @"Address is required.")]
        public string Address
        {
            get { return _company.Address; }
            set
            {
                _company.Address = value;
                OnPropertyChanged(value);
            }
        }

        [Required(ErrorMessage = @"Website is required.")]
        [RegularExpression(@"^http(s)?://[a-z0-9-]+(.[a-z0-9-]+)*(:[0-9]+)?(/.*)?$",
            ErrorMessage = @"The format of the web address is not valid")]
        public string Website
        {
            get { return _company.Website; }
            set
            {
                _company.Website = value;
                OnPropertyChanged(value);
            }
        }

        public string Contact
        {
            get { return _company.Contact; }
            set
            {
                _company.Contact = value;
                OnPropertyChanged(value);
            }
        }

        public ICommand SaveCommand { get; private set; }

        private void Save()
        {
            MessageBox.Show("Your changes where saved.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CanSave
        {
            get { return !_validation.HasErrors; }
        }

        protected void OnPropertyChanged(object value, [CallerMemberName] string propertyName = "")
        {
            _validation.ValidateProperty(propertyName, value);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(new PropertyChangedEventArgs("CanSave"));
        }

        #region Validation

        private readonly ValidationAdapter _validation = new ValidationAdapter();

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { return string.Join(Environment.NewLine, _validation.GetPropertyError(columnName)); }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

        #endregion
    }
}
