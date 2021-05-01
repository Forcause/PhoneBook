using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using practice6WPF.Model;

namespace practice6WPF.ViewModel
{
    public class PhoneBookViewModel : INotifyPropertyChanged
    {

        public PhoneBookViewModel(IDialogService dialogService, JsonFileService jsonFileService, XMLFileService xmlfileService)
        {
            this._dialogService = dialogService;
            this._jsonFileService = jsonFileService;
            this._xmlfileService = xmlfileService;
            PhoneBook = new ObservableCollection<Contact>
            {
                new Contact {NamePatron = "George", SurName = "Higgs", PhoneNumber = "89313074443"},
                new Contact {NamePatron = "Iya", SurName = "Mironova", PhoneNumber = "89214782812", Email = "iyamir@bk.ru"},
                new Contact {NamePatron = "Igor", SurName = "Lapin", PhoneNumber = "89216759423", Email = "igolapi@gmail.com"}
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private Contact selectedContact;
        
        private BaseCommand addCommand;
        private BaseCommand deleteCommand;
        private BaseCommand saveCommand;
        private BaseCommand loadCommand;
        
        private IDialogService _dialogService;
        private JsonFileService _jsonFileService;
        private XMLFileService _xmlfileService;

        public Contact SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        public ObservableCollection<Contact> PhoneBook { get; set; }

        
        void OnPropertyChanged(string properety = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(properety));
        }
        
        public BaseCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new BaseCommand(obj =>
                {
                    Contact contact = new Contact();
                    PhoneBook.Insert(0, contact);
                    SelectedContact = contact;
                }));
            }
        }

        public BaseCommand DeleteCommand
        {
            get
            {
                if (deleteCommand != null)
                    return deleteCommand;
                else
                {
                    Action<object> Execute = o =>
                    {
                        Contact con = (Contact) o;
                        PhoneBook.Remove(con);
                    };
                    Func<object, bool> CanExecute = o => PhoneBook.Count > 0;
                    deleteCommand = new BaseCommand(Execute, CanExecute);
                    return deleteCommand;
                }
            }
        }

        public BaseCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new BaseCommand(obj =>
                {
                    try
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            switch (_dialogService.FilterIndex)
                            {
                                case 1:
                                    _xmlfileService.Save(_dialogService.FilePath, PhoneBook);
                                    break;
                                case 2:
                                    _jsonFileService.Save(_dialogService.FilePath, PhoneBook);
                                    break;
                            }
                            _dialogService.ShowMessage("Save complete");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }

        public BaseCommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new BaseCommand(obj =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog() == true)
                        {
                            switch (_dialogService.FilterIndex)
                            {
                                case 1:
                                    var xmlPhones = _xmlfileService.Open(_dialogService.FilePath);
                                    PhoneBook.Clear();
                                    foreach (var p in xmlPhones)
                                        PhoneBook.Add(p);
                                    _dialogService.ShowMessage("XML load complete");
                                    break;
                                case 2:
                                    var jsPhones = _jsonFileService.Open(_dialogService.FilePath);
                                    PhoneBook.Clear();
                                    foreach (var p in jsPhones)
                                        PhoneBook.Add(p);
                                    _dialogService.ShowMessage("Load complete");
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }
    }
}