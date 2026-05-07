using WheatGrainClassifierWpfApp.Db;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Helpers
{
    public class ExperiementRepo
    {
        public  static void Save(int k, string[] classesName, string distance, double accuracy, int trainSize, int testSize, int userId, string userName)
        {
            Experiment experiment = new Experiment
            {
                KValue = k,
                ClassesName = classesName,
                DistanceValue = distance,
                Accuracy = accuracy,
                TrainSize = trainSize,
                TestSize = testSize,
                UserId = userId,
                UserName = userName
            };

            WheatGrainClassifierDbContext context = new WheatGrainClassifierDbContext();

            context.Experiments.Add(experiment);
            context.SaveChanges();

        }
    }
}
