
namespace w12.Models
{
    public class ExecutionExerciseView
    {
        public BaseExercise? BaseExercise { get; set; }
        public int BaseExerciseId { get; set; }
        public DateTime ExecutionDate { get; set; }

        public string NumberOfSets { get; set; } = string.Empty;

        public string RepetitionCount { get; set; } = string.Empty;

        public string Weight { get; set; } = string.Empty;

        public string? Observations { get; set; }
    }
}
