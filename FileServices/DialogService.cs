using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace practice6WPF
{
    public class DialogSerivice : IDialogService
    {
        public string FilePath { get; set; }
        public int FilterIndex { get; set; }
        public bool OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.CurrentDirectory;
            ofd.Filter = "Файл в xml|*.xml|Файл в json|*.json";
            if (ofd.ShowDialog() == true)
            {
                FilePath = ofd.FileName;
                FilterIndex = ofd.FilterIndex;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            sfd.Filter = "Файл в xml|*.xml|Файл в json|*.json";
            if (sfd.ShowDialog() == true)
            {
                FilterIndex = sfd.FilterIndex;
                FilePath = sfd.FileName;
                return true;
            }

            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}