using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using BirthdayApp.Tools;
using BirthdayApp.Tools.Managers;

namespace BirthdayApp
{
    internal class BirthdayViewModel : INotifyPropertyChanged, ILoaderOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        private string _age;
        private string _birthday;
        private string _zodiac;
        private string _animal;

        private RelayCommand<object> _searchCommand;
        #endregion

        public BirthdayViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }

        #region Properties
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Zodiac
        {
            get { return _zodiac; }
            set { _zodiac = value; OnPropertyChanged(); }
        }

        public string Animal
        {
            get { return _animal; }
            set { _animal = value; OnPropertyChanged(); }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(); }
        }

        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand<object>(
                           SearchingImplementation, o => CanExecuteCommand()));
            }
        }
        #endregion

        private async void SearchingImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            await Task.Run(() =>
            {
                SetNothing();
                DateTime birthday = Convert.ToDateTime(_birthday);
                if (!CheckBirthday(birthday))
                {
                    LoaderManager.Instance.HideLoader();
                    return;
                }
                if (DateTime.Today.Month == birthday.Month && DateTime.Today.Day == birthday.Day)
                {
                    MessageBox.Show("Happy Birthday!!!");
                }

                SetCalculatedAge(birthday);
                SetCalculatedZodiac(birthday);
                SetCalculatedAnimal(birthday);
            });

            LoaderManager.Instance.HideLoader();
        }

        private bool CheckBirthday(DateTime birthday)
        {
            if (DateTime.Today.Year < birthday.Year)
            {
                MessageBox.Show("There must be a mistake. The birthday can't be yet to come.");
                return false;
            }
            if (DateTime.Today.Year == birthday.Year)
            {
                if (DateTime.Today.Month < birthday.Month)
                {
                    MessageBox.Show("There must be a mistake. The birthday can't be yet to come.");
                    return false;
                }
                if (DateTime.Today.Month == birthday.Month && DateTime.Today.Day < birthday.Day)
                {
                    MessageBox.Show("There must be a mistake. The birthday can't be yet to come.");
                    return false;
                }
            }

            int years = DateTime.Today.Year - birthday.Year;
            if (DateTime.Today.Month < birthday.Month)
            {
                years--;
            }
            else if (DateTime.Today.Month == birthday.Month && DateTime.Today.Day < birthday.Day)
            {
                years--;
            }

            if (years > 135)
            {
                MessageBox.Show("There must be a mistake. You can't be older than 135 years.");
                return false;
            }

            return true;
        }

        public bool CanExecuteCommand()
        {
            return !String.IsNullOrWhiteSpace(_birthday);
        }

        private void SetCalculatedAge(DateTime birthday)
        {
            int years = DateTime.Today.Year - birthday.Year;
            if (DateTime.Today.Month < birthday.Month)
            {
                years--;
            }
            else if (DateTime.Today.Month == birthday.Month)
            {
                if (DateTime.Today.Day < birthday.Day)
                {
                    years--;
                }
            }

            Age = years.ToString();
        }

        private void SetCalculatedZodiac(DateTime birthday)
        {
            switch (birthday.Month)
            {
                case 1:
                    Zodiac = (birthday.Day < 20) ? "Capricorn" : "Aquarius";
                    break;
                case 2:
                    Zodiac = (birthday.Day < 19) ? "Aquarius" : "Pisces";
                    break;
                case 3:
                    Zodiac = (birthday.Day < 21) ? "Pisces" : "Aries";
                    break;
                case 4:
                    Zodiac = (birthday.Day < 20) ? "Aries" : "Taurus";
                    break;
                case 5:
                    Zodiac = (birthday.Day < 21) ? "Taurus" : "Gemini";
                    break;
                case 6:
                    Zodiac = (birthday.Day < 21) ? "Gemini" : "Cancer";
                    break;
                case 7:
                    Zodiac = (birthday.Day < 23) ? "Cancer" : "Leo";
                    break;
                case 8:
                    Zodiac = (birthday.Day < 23) ? "Leo" : "Virgo";
                    break;
                case 9:
                    Zodiac = (birthday.Day < 23) ? "Virgo" : "Libra";
                    break;
                case 10:
                    Zodiac = (birthday.Day < 23) ? "Libra" : "Scorpio";
                    break;
                case 11:
                    Zodiac = (birthday.Day < 22) ? "Scorpio" : "Sagittarius";
                    break;
                case 12:
                    Zodiac = (birthday.Day < 22) ? "Sagittarius" : "Capricorn";
                    break;
            }
        }

        private void SetCalculatedAnimal(DateTime birthday)
        {
            int num = birthday.Year % 12;
            switch (num)
            {
                case 0: Animal = "Monkey"; break;
                case 1: Animal = "Rooster"; break;
                case 2: Animal = "Dog"; break;
                case 3: Animal = "Pig"; break;
                case 4: Animal = "Rat"; break;
                case 5: Animal = "Ox"; break;
                case 6: Animal = "Tiger"; break;
                case 7: Animal = "Rabbit"; break;
                case 8: Animal = "Dragon"; break;
                case 9: Animal = "Snake"; break;
                case 10: Animal = "Horse"; break;
                case 11: Animal = "Goat"; break;
            }
        }

        private void SetNothing()
        {
            Animal = "";
            Age = "";
            Zodiac = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
