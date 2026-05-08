using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WheatGrainClassifierWpfApp.Commands;
using WheatGrainClassifierWpfApp.Models;
using WheatGrainClassifierWpfApp.Services;
 
namespace WheatGrainClassifierWpfApp.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly ApiService _apiService = new ApiService();
 
        private ObservableCollection<User> _users = new();
        public ObservableCollection<User> Users
        {
            get => _users;
            set { _users = value; OnPropertyChanged(); }
        }
 
        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }
 
        private string _statusMessage = "Cliquez sur 'Charger' pour récupérer les utilisateurs.";
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }
 
        public ICommand LoadUsersCommand { get; }
 
        public UsersViewModel()
        {
            LoadUsersCommand = new RelayCommand(LoadUsers);
        }
 
        private void LoadUsers()
        {
            try
            {
                var users = _apiService.GetUsers("users");
                Users         = new ObservableCollection<User>(users);
                StatusMessage = $"{users.Count} utilisateurs chargés depuis dummyjson.com";
            }
            catch (Exception ex)
            {
                StatusMessage = "Erreur lors du chargement.";
                MessageBox.Show($"Erreur API : {ex.Message}", "Erreur réseau",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}