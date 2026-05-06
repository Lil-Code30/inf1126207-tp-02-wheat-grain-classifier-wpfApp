

using System.Collections.ObjectModel;
using WheatGrainClassifierWpfApp.Helpers;
using WheatGrainClassifierWpfApp.Interfaces;
using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Services
{
    public class KnnService
    {
        private int k;
        private IDistance distance; // Euclidian ou Manhattan
        private ObservableCollection<Grain> ListGrainApprentisage; // apprentisage

        public KnnService(int k, IDistance distance, ObservableCollection<Grain> trainData)
        {
            this.k = k;
            this.distance = distance;
            ListGrainApprentisage = trainData;
        }


        /// <summary>
        /// Cherche le k plus proche voisin
        /// </summary>
        /// <param name="sample">un exemplaire de grain</param>
        /// <returns>Liste de voisins</returns> 
        private List<Voisin> KPlusProcheVoisin(Grain sample)
        {
            List<Voisin> voisins = new List<Voisin>();

            foreach (Grain grainApprentisage in ListGrainApprentisage)
            {
                double d = distance.Distance(sample, grainApprentisage);

                // creation du voisin 
                Voisin voisin = new Voisin { Grain = grainApprentisage, Distance = d };

                voisins.Add(voisin);
            }

            // algo de tri rapide
            IAlgorithmeTri triRapide = new TriRapide();

            triRapide.Trier(voisins);

            List<Voisin> kPlusProcheVoisins;

            if (voisins.Count <= k)
            {
                kPlusProcheVoisins = new List<Voisin>(voisins);  // copia toda la lista
            }
            else
            {
                kPlusProcheVoisins = voisins.GetRange(0, k);
            }

            return kPlusProcheVoisins;
        }

        /// <summary>
        /// Predire une grain inconnu
        /// </summary>
        /// <param name="inconnu">un exemplaire de grain a predit</param>
        /// <returns>le nom de la prediction</returns>
        public string Predire(Grain inconnu)
        {
            List<Voisin> kPlusProcheVoisins = KPlusProcheVoisin(inconnu);
            if (kPlusProcheVoisins.Count == 0)
            {
                return "Inconnu";
            }

            //comptage des votes
            Dictionary<string, int> compteur = new Dictionary<string, int>();

            foreach (Voisin v in kPlusProcheVoisins)
            {
                string variety = v.Grain.Variety;

                if (compteur.ContainsKey(variety))
                {
                    compteur[variety] = compteur[variety] + 1; ;
                }
                else
                {
                    compteur[variety] = 1;
                }
            }

            //variety gagnante
            string varietyGagnante = "";
            int maxvotes = -1;

            foreach (var paire in compteur)
            {
                if (paire.Value > maxvotes)
                {
                    maxvotes = paire.Value;
                    varietyGagnante = paire.Key;
                }
            }

            return varietyGagnante;
        }
    }
}
