using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WheatGrainClassifierWpfApp.Commands;
using WheatGrainClassifierWpfApp.Helpers;
using WheatGrainClassifierWpfApp.Interfaces;
using WheatGrainClassifierWpfApp.Models;
using WheatGrainClassifierWpfApp.Services;

namespace WheatGrainClassifierWpfApp.ViewModels
{
    public class CalculationViewModel: BaseViewModel
    {
        private int _k;
        private string _selectedDistance;
        private double _exactitude;
        private int[,] _matriceDeConfusion;
        string[] classNames = new string[] { "Canadian", "Kama", "Rosa", };


        // collection observable 
        private ObservableCollection<Grain> _trainData;
        private ObservableCollection<Grain> _testData;
        public ObservableCollection<string> Distances { get; } = new ObservableCollection<string>() { "Distance Euclidienne", "Distance de Manhattan" };
        public ObservableCollection<ConfusionRow> ConfusionMatrix { get; } = new();

        private string _trainFilePath;
        private string _testFilePath;

        // Api Service
        private readonly ApiService _apiService = new ApiService();
        private ObservableCollection<User> _users;
        private User _selectedUser;


        // getters et setters
        public int K
        {
            get => _k;
            set
            {
                if(value != _k && value > 0)
                {
                    _k = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedDistance
        {
            get => _selectedDistance;
            set
            {
                if(value != _selectedDistance)
                {
                    _selectedDistance = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Exactitude
        {
            get => _exactitude;
            set
            {
                _exactitude = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Grain> TrainData
        {
            get => _trainData;
            set
            {
                if (_trainData != value)
                {
                    _trainData = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Grain> TestData
        {
            get => _testData;
            set
            {
                if (_testData != value)
                {
                    _testData = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TestFilePath
        {
            get => _testFilePath;
            set
            {
                if (_testFilePath != value)
                {
                    _testFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TrainFilePath
        {
            get => _trainFilePath;
            set
            {
                if (_trainFilePath != value)
                {
                    _trainFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if(value != _users)
                {
                    _users = value;
                    OnPropertyChanged();
                }
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if(_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commandes
        public ICommand LoadTrainCommand { get; }
        public ICommand LoadTestCommand { get; }
        public ICommand RunCommand { get; }
        public ICommand LaodUsersCommand { get; }

        public CalculationViewModel()
        {
            LoadTrainCommand = new RelayCommand(LoadTrainFile);
            LoadTestCommand = new RelayCommand(LoadTestFile);
            RunCommand = new RelayCommand(RunClassification, CanRunClassification);
            LaodUsersCommand = new RelayCommand(LoadUsers);
        }

        // chargement des fichiers test et apprentisage
        private void LoadTrainFile()
        {
            var dialog = new OpenFileDialog { Filter = "CSV Files (*.csv)|*.csv" };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    TrainFilePath = dialog.FileName;

                    var grains = CsvUtils.Load(TrainFilePath);
                    TrainData = new ObservableCollection<Grain>(grains);
                    MessageBox.Show($"{TrainData.Count} échantillons chargés.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement : {ex.Message}",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadTestFile()
        {
            var dialog = new OpenFileDialog { Filter = "CSV Files (*.csv)|*.csv" };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    TestFilePath = dialog.FileName;

                    var grains = CsvUtils.Load(TestFilePath);
                    TestData = new ObservableCollection<Grain>(grains);
                    MessageBox.Show($"{TestData.Count} échantillons chargés.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du chargement : {ex.Message}",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Vérification avant execution
        private bool CanRunClassification() => TrainData?.Count > 0 && TestData?.Count > 0 && K > 0 && SelectedDistance != null && SelectedUser != null;

        // Entraînement et Prédiction
        private void RunClassification()
        {
            try
            {
                // choix de la metrique de distance a utiliser 
                IDistance distance;
                if (SelectedDistance == "Distance Euclidienne")
                {
                    distance = new EuclideanDistance();
                }
                else
                {
                    distance = new ManhattanDistance();
                }

                KnnService knn = new KnnService(K, distance, TrainData);

                // liste des labels
                List<string> predictions = new List<string>();
                List<string> actuels = new List<string>();

                foreach (Grain grainTest in TestData)
                {
                    string prediction = knn.Predire(grainTest);
                    predictions.Add(prediction);
                    actuels.Add(grainTest.Variety);
                }

                // calcul de la performances du modèle
                Exactitude = PerformanceService.Exactitude(predictions, actuels);

                _matriceDeConfusion = PerformanceService.MatriceDeConfusion(actuels, predictions, classNames.Length);

                // mis a jour de 'ConfusionMatrix' avec une nouvelle matrix apres calcul
                UpdateConfusionMatrix(_matriceDeConfusion);

                // sauvegarde dans la db
                ExperiementRepo.Save(K, classNames, SelectedDistance, Exactitude, TrainData.Count, TestData.Count, SelectedUser.Id, SelectedUser.FullName);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'entraînement et la prédiction : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// mets a jour la Matrix de confusion  avec une nouvelle matrix
        /// </summary>
        /// <param name="matrice"></param>
        private void UpdateConfusionMatrix(int[,] matrice)
        {
            ConfusionMatrix.Clear();

            ConfusionMatrix.Add(new ConfusionRow
            {
                Variety = "Canadian",
                Canadian = matrice[0, 0],
                Kama = matrice[0, 1],
                Rosa = matrice[0, 2]
            });

            ConfusionMatrix.Add(new ConfusionRow
            {
                Variety = "Kama",
                Canadian = matrice[1, 0],
                Kama = matrice[1, 1],
                Rosa = matrice[1, 2]
            });

            ConfusionMatrix.Add(new ConfusionRow
            {
                Variety = "Rosa",
                Canadian = matrice[2, 0],
                Kama = matrice[2, 1],
                Rosa = matrice[2, 2]
            });
        }

        private void LoadUsers()
        {
            
            try
            {
                var users = _apiService.GetUsers("users");
                Users = new ObservableCollection<User>(users);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur API : {ex.Message}", "Erreur réseau",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
