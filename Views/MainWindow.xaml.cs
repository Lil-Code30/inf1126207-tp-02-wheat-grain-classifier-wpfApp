using System.Windows;
 
namespace WheatGrainClassifierWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainViewModel();
        }
 
        private void MenuItem_Quitter(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Voulez-vous quitter l'application ?",
                "Quitter", MessageBoxButton.YesNo, MessageBoxImage.Question);
 
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}