namespace WheatGrainClassifierWpfApp.Models
{
    public class Grain
    {
        public string Variety { get; set; }
        public double Area { get; init; }
        public double Perimeter { get; init; }
        public double Compactness { get; init; }
        public double Kernel_Length { get; init; }
        public double Kernel_Width { get; init; }
        public double Asymmetry_Coefficient { get; init; }
        public double Groove_Length { get; init; }
    }
}
