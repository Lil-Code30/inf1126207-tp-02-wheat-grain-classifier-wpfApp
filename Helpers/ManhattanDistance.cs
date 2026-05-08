using WheatGrainClassifierWpfApp.Interfaces;
using WheatGrainClassifierWpfApp.Models;
 
namespace WheatGrainClassifierWpfApp.Helpers
{
    public class ManhattanDistance : IDistance
    {
        public double Distance(Grain a, Grain b)
        {
           
            double sum = 0;
            sum += Math.Abs(a.Area - b.Area);
            sum += Math.Abs(a.Perimeter - b.Perimeter);
            sum += Math.Abs(a.Compactness - b.Compactness);
            sum += Math.Abs(a.Kernel_Length - b.Kernel_Length);
            sum += Math.Abs(a.Kernel_Width - b.Kernel_Width);
            sum += Math.Abs(a.Asymmetry_Coefficient - b.Asymmetry_Coefficient);
            sum += Math.Abs(a.Groove_Length - b.Groove_Length);
            return sum;
        }
    }
}

