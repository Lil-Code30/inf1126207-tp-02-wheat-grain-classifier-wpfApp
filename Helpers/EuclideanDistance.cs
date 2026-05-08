using WheatGrainClassifierWpfApp.Interfaces;
using WheatGrainClassifierWpfApp.Models;
 
namespace WheatGrainClassifierWpfApp.Helpers
{
    public class EuclideanDistance : IDistance
    {
        public double Distance(Grain a, Grain b)
        {
            
            double sum = 0;
            sum += Math.Pow(a.Area - b.Area, 2);
            sum += Math.Pow(a.Perimeter - b.Perimeter, 2);
            sum += Math.Pow(a.Compactness - b.Compactness, 2);
            sum += Math.Pow(a.Kernel_Length - b.Kernel_Length, 2);
            sum += Math.Pow(a.Kernel_Width - b.Kernel_Width, 2);
            sum += Math.Pow(a.Asymmetry_Coefficient - b.Asymmetry_Coefficient, 2);
            sum += Math.Pow(a.Groove_Length - b.Groove_Length, 2);
            return Math.Sqrt(sum);
        }
    }
}

