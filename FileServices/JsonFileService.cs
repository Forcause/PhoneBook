using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using practice6WPF.Model;
using Newtonsoft.Json;

namespace practice6WPF
{
    public class JsonFileService : IFIleService
    {
        public ObservableCollection<Contact> Open(string filename)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filename);
                var read = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(read);
            }
            finally
            {
                if(reader != null)
                    reader.Close();
            }
        }

        public void Save(string filename, ObservableCollection<Contact> phoneBookContacts)
        {
            TextWriter writer = null;
            try
            {
                string json = JsonConvert.SerializeObject(phoneBookContacts, Formatting.Indented);
                writer = new StreamWriter(filename);
                writer.Write(json);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}