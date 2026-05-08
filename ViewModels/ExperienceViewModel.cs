using System.Collections.ObjectModel;
using WheatGrainClassifierWpfApp.Db;
using WheatGrainClassifierWpfApp.Helpers;
using WheatGrainClassifierWpfApp.Models;
 
namespace WheatGrainClassifierWpfApp.ViewModels
{
    public class ExperienceViewModel : BaseViewModel
    {
        ExperimentRepo experimentRepo = new ExperimentRepo(new WheatGrainClassifierDbContext());
 
        private ObservableCollection<Experiment> _experiments = new();
        public ObservableCollection<Experiment> Experiments
        {
            get => _experiments;
            set
            {
                if (value != null)
                {
                    _experiments = value;
                    OnPropertyChanged();
                }
            }
        }
 
        public ExperienceViewModel()
        {
            Refresh();
        }
 
        
        public void Refresh()
        {
            List<Experiment> experiments = experimentRepo.Load();
            Experiments = new ObservableCollection<Experiment>(experiments);
        }
    }
}
