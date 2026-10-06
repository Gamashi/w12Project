
namespace w12.Models
{
    public class OneRepMaxCardSummary
    {
        public string Title { get; set; } = "RECORDE PESSOAL (1RM)";
        public string ExerciseName { get; set; } = "Nenhum exercício registrado";
        public double Estimated1RM { get; set; }
        public bool HasData { get; set; }
    }
}
