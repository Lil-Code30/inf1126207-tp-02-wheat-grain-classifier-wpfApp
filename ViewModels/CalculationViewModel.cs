using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WheatGrainClassifierWpfApp.Commands;
using WheatGrainClassifierWpfApp.Helpers;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.ViewModels
{
    public class CalculationViewModel: BaseViewModel
    {
        private int _k;
        private string _selectedDistance;
        // collection observable 
        private ObservableCollection<Grain> _trainData;
        private ObservableCollection<Grain> _testData;
        public ObservableCollection<string> Distances { get; } = new ObservableCollection<string>() { "Euclidienne", "Manhattan" };
        private string _trainFilePath;
        private string _testFilePath;

        // getter et setters
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

        // Commandes
        public ICommand LoadTrainCommand { get; }
        public ICommand LoadTestCommand { get; }

        public CalculationViewModel()
        {
            LoadTrainCommand = new RelayCommand(LoadTrainFile);
            LoadTestCommand = new RelayCommand(LoadTestFile);
        }

        //fontions pour les commands
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
    }
}
