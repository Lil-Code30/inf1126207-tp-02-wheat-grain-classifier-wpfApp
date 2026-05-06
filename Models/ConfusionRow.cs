namespace WheatGrainClassifierWpfApp.Models
{
    public class ConfusionRow
    {
        public string Variety { get; init; } = string.Empty;
        public int Canadian { get; init; }
        public int Kama { get; init; }
        public int Rosa { get; init; }
    }
}
