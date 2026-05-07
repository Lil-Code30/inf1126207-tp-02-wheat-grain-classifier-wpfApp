using System.Collections.ObjectModel;
using WheatGrainClassifierWpfApp.Db;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Helpers
{
    public class ExperimentRepo
    {
        private readonly WheatGrainClassifierDbContext _context;

        public ExperimentRepo(WheatGrainClassifierDbContext context)
        {
            _context = context;
        }

        public void Save(int k, string[] classesName, string distance, double accuracy, int[,] confusionMatrix, int trainSize, int testSize, int userId, string userName)
        {
            Experiment experiment = new Experiment
            {
                KValue = k,
                ClassesName = classesName,
                DistanceValue = distance,
                Accuracy = accuracy,
                ConfusionMatrix = confusionMatrix,
                TrainSize = trainSize,
                TestSize = testSize,
                UserId = userId,
                UserName = userName
            };

            _context.Experiments.Add(experiment);
            _context.SaveChanges();

        }

        public List<Experiment> Load()
        {
            var list = _context.Experiments
                     .OrderByDescending(e => e.ExecutionDate)
                     .ToList();

            return list;
        }
    }
}
