using SQLite;
namespace w12.Models
{
    public class BaseExercise
    {
        [PrimaryKey, AutoIncrement] 
        public int BaseExerciseId { get; set; }
        [NotNull]
        public string Name { get;set ; }
        public string Descripton { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }      
    }
}
