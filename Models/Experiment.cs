using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheatGrainClassifierWpfApp.Models
{
    public class Experiment
    {
        private int[,]? _confusionMatrix;

        [Key]
        public int Id { get; set; }

        [Required]
        public int KValue { get; set; }

        [Required]
        public string[] ClassesName { get; set; }

        [Required]
        public string DistanceValue { get; set; }

        [Required]
        public double Accuracy { get; set; }

        [NotMapped]
        public int[,]? ConfusionMatrix
        {
            get => _confusionMatrix;
            set
            {
                _confusionMatrix = value;
                ConfusionMatrixDisplay = FormatConfusionMatrix(value);
            }
        }

        [Required]
        public int TrainSize { get; set; }

        [Required]
        public int TestSize { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }


        // Formattage pour l'afffichage dans le DataGrid et db
        public string ClassesDisplay => string.Join(", ", ClassesName ?? []);

        public string ConfusionMatrixDisplay { get; set; } = string.Empty;
        private static string FormatConfusionMatrix(int[,]? confusionMatrix)
        {
            if (confusionMatrix is null)
            {
                return string.Empty;
            }

            var rowCount = confusionMatrix.GetLength(0);
            var colCount = confusionMatrix.GetLength(1);
            var rows = new string[rowCount];

            for (int r = 0; r < rowCount; r++)
            {
                var cols = new string[colCount];

                for (int c = 0; c < colCount; c++)
                {
                    cols[c] = confusionMatrix[r, c].ToString();
                }

                rows[r] = string.Join(" ", cols);
            }

            return string.Join(" | ", rows);
        }
    }

}
