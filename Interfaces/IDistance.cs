using WheatGrainClassifierWpfApp.Models;

namespace WheatGrainClassifierWpfApp.Interfaces
{
    public interface IDistance
    {
        double Distance(Grain a, Grain b);
    }
}
