using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using practice6WPF.Model;

namespace practice6WPF
{
    public class XMLFileService : IFIleService
    {
        public ObservableCollection<Contact> Open(string filename)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filename);
                ObservableCollection<Contact> doneBase = new ObservableCollection<Contact>();
                var read = reader.ReadToEnd();
                var x = XElement.Parse(read);
                var res = from e in x.Elements()
                    select new Contact(e.Element("NamePatron").Value, e.Element("SurName").Value,
                        e.Element("PhoneNumber").Value, e.Element("Email").Value);
                foreach (var contact in res)
                {
                    doneBase.Add(contact);
                }
                return doneBase;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void Save(string filename, ObservableCollection<Contact> phoneBookContacts)
        {
            TextWriter writer = null;
            try
            {
                var x = new XElement("PhoneBook",
                    from contact in phoneBookContacts
                    select new XElement("Contact",
                        new XElement("NamePatron", contact.NamePatron),
                        new XElement("SurName", contact.SurName),
                        new XElement("PhoneNumber", contact.SurName),
                        new XElement("Email", contact.Email)));
                string s = x.ToString();
                writer = new StreamWriter(filename);
                writer.Write(s);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}