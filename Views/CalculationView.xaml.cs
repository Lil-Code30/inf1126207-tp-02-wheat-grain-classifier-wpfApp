using System.Windows.Controls;


namespace WheatGrainClassifierWpfApp.Views
{
   
    public partial class CalculationView : UserControl
    {
        public CalculationView()
        {
            InitializeComponent();
            DataContext = new ViewModels.CalculationViewModel();
        }
    }
}
