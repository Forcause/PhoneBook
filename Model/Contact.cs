using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using practice6WPF.ViewModel;

namespace practice6WPF.Model
{
    public class Contact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string namePatron;
        private string surName;
        private string phoneNumber;
        private string email;
        private string fullName;

        public Contact(string namePatron, string surName, string phoneNumber, string email)
        {
            this.namePatron = namePatron;
            this.surName = surName;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        public Contact()
        {
            this.namePatron = "";
            this.surName = "";
            this.phoneNumber = "";
            this.email = "";
        }

        
        public string NamePatron
        {
            get => namePatron;
            set
            {
                namePatron = value;
                fullName = SetFullName(surName, namePatron);
                OnPropertyChanged("NamePatron");
                OnPropertyChanged("FullName");
            }
        }
        public string SurName
        {
            get => surName;
            set
            {
                surName = value;
                fullName = SetFullName(surName, namePatron);
                OnPropertyChanged("SurName");
                OnPropertyChanged("FullName");
            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string FullName
        {
            get => surName + " " + namePatron;
            set
            {
                fullName = SetFullName(surName, namePatron); 
                OnPropertyChanged("FullName");
            }
        }

        private string SetFullName(string surName, string namePatron)
        {
            return surName + " " + namePatron;
        }

        void OnPropertyChanged(string properety = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properety));
        }
    }
}