using SQLite;
namespace w12.Models
{
    public class BaseExercise
    {
        [PrimaryKey, AutoIncrement] 
        public int BaseExerciseId { get; set; }
        [NotNull]
        public string? Name { get;set ; }
        public string? Description { get; set; }
        [Indexed]
        public Category? Category { get; set; }
        public string? Image { get; set; }      
        public string ? Video { get; set; } 
    }
}
