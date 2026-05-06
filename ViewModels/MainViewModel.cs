
using System.Windows.Input;
using WheatGrainClassifierWpfApp.Commands;

namespace WheatGrainClassifierWpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand showCalculationCommand { get; }
        public ICommand ShowExperienceCommand { get; }

        public MainViewModel()
        {
            // View de Calcule par defaut
            CurrentView = new CalculationViewModel();

            showCalculationCommand = new RelayCommand(ShowCalculation);
            ShowExperienceCommand = new RelayCommand(ShowExperience);

        }

        private void ShowCalculation()
        {
            CurrentView = new CalculationViewModel();
        }

        private void ShowExperience()
        {
            CurrentView = new ExperienceViewModel();
        }
    }
}
