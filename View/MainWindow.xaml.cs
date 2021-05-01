using System.Windows;
using practice6WPF.ViewModel;

namespace practice6WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new PhoneBookViewModel(new DialogSerivice(), new JsonFileService(), new XMLFileService());
        }
    }
}