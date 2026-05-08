
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
 
        private readonly UsersViewModel _usersVM;
 
        public ICommand showCalculationCommand { get; }
        public ICommand ShowExperienceCommand  { get; }
        public ICommand ShowUsersCommand       { get; }
 
        public MainViewModel()
        {
            _usersVM = new UsersViewModel();
 
            
            CurrentView = new CalculationViewModel();
 
            showCalculationCommand = new RelayCommand(ShowCalculation);
            ShowExperienceCommand  = new RelayCommand(ShowExperience);
            ShowUsersCommand       = new RelayCommand(ShowUsers);
        }
 
        private void ShowCalculation()
        {
            CurrentView = new CalculationViewModel();
        }
 
        private void ShowExperience()
        {
            CurrentView = new ExperienceViewModel();
        }
 
        private void ShowUsers()
        {
            CurrentView = _usersVM;
        }
    }
}