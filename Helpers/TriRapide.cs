using WheatGrainClassifierWpfApp.Interfaces;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Helpers
{
    public class TriRapide : IAlgorithmeTri
    {
        public void Trier(List<Voisin> voisins)
        {
            if (voisins.Count == 0)
            {
                return;
            }

            Trier(voisins, 0, voisins.Count - 1);
        }

        private static void Trier(List<Voisin> voisins, int beg, int end)
        {
            if (beg < end)
            {
                int partitionIndex = Partition(voisins, beg, end);
                Trier(voisins, beg, partitionIndex - 1);
                Trier(voisins, partitionIndex + 1, end);
            }
        }

        private static int Partition(List<Voisin> voisins, int beg, int end)
        {
            Voisin pivot = voisins[end];
            int lowestIndex = beg;
            Voisin temp;

            for (int i = beg; i <= end - 1; i++)
            {
                if (voisins[i].Distance <= pivot.Distance)
                {
                    temp = voisins[lowestIndex];
                    voisins[lowestIndex] = voisins[i];
                    voisins[i] = temp;

                    lowestIndex++;
                }
            }

            temp = voisins[lowestIndex];
            voisins[lowestIndex] = voisins[end];
            voisins[end] = temp;

            return lowestIndex;
        }
    }
}
