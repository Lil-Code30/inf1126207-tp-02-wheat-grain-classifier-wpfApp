
namespace WheatGrainClassifierWpfApp.Services
{
    public class PerformanceService
    {
        /// <summary>
        /// Calcul de la précision du model de prédictions
        /// </summary>
        /// <param name="actuels"></param>
        /// <param name="predictions"></param>
        /// <returns>precision</returns>
        /// <exception cref="Exception"></exception>
        public static double Exactitude(List<string> actuels, List<string> predictions)
        {
            int prediction_correct = 0;

            if (actuels.Count != predictions.Count)
            {
                throw new Exception("Les listes des valeurs actuelles et des prédictions doivent avoir la même taille.");
            }

            for (int i = 0; i < predictions.Count; i++)
            {
                string prediction = predictions[i];
                string actuel = actuels[i];

                if (string.Equals(actuel, prediction, StringComparison.OrdinalIgnoreCase))
                {
                    prediction_correct++;
                }
            }

            double precision = ((double)prediction_correct / actuels.Count) * 100;
            return precision;
        }

        /// <summary>
        /// Calcule la matrice de confusion pour un problème de classification à 3 classes
        /// </summary>
        /// <param name="actuels"></param>
        /// <param name="predictions"></param>
        /// <param name="nbrVariety"></param>
        /// <returns>matrice</returns>
        public static int[,] MatriceDeConfusion(List<string> actuels, List<string> predictions, int nbrVariety)
        {
            if (actuels.Count != predictions.Count)
            {
                throw new Exception("Les listes des valeurs actuelles et des prédictions doivent avoir la même taille.");
            }

            int[,] matrice = new int[nbrVariety, nbrVariety];

            var varietyToIndex = new Dictionary<string, int>
        {
            { "Canadian", 0 },
            { "Kama", 1 },
            { "Rosa", 2 },
        };

            for (int i = 0; i < actuels.Count; i++)
            {
                string actuelVariety = actuels[i];
                string predictedVariety = predictions[i];

                if (varietyToIndex.TryGetValue(actuelVariety, out int actualIdx) &&
                    varietyToIndex.TryGetValue(predictedVariety, out int predictedIdx))
                {
                    matrice[actualIdx, predictedIdx]++;
                }
            }

            return matrice;
        }
    }
}
