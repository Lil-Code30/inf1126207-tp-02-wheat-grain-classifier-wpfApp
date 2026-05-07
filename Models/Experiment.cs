using System.ComponentModel.DataAnnotations;

namespace WheatGrainClassifierWpfApp.Models
{
    public class Experiment
    {
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
    }

}
