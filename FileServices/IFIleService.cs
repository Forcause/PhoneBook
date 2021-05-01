using System.Collections.ObjectModel;
using practice6WPF.Model;

namespace practice6WPF
{
    public interface IFIleService
    {
        ObservableCollection<Contact> Open(string filename);
        void Save(string filename, ObservableCollection<Contact> phoneBookContacts);
    }
}