using SQLite;

namespace w12.Models
{
    public class ExecutionExercise
    {
        [PrimaryKey, AutoIncrement]
        public int ExerciseId { get; set; }

        [Indexed]
        public int UserId { get; set; }

        [Indexed]
        public int ExerciseSessionId { get; set; }
        [Ignore]
        public BaseExercise? BaseExercise { get; set; }
        [Indexed]                
        public int BaseExerciseId { get; set; }
        public DateTime ExecutionDate { get; set; }

        public int NumberOfSets { get; set; }

        public int RepetitionCount { get; set; }

        public double Weight { get; set; }

        public string? Observations { get; set; }
    }
}
