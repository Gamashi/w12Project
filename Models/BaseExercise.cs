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
        // 1. Esta é a coluna que realmentes vai pro SQLite!
        [Indexed]
        public int CategoryId { get; set; }

        // 2. Mantemos [Ignore] no objeto para a UI/Binding no Picker se quiser
        [Ignore]
        public Category? Category { get; set; }
        public string? Image { get; set; }      
        public string ? Video { get; set; } 
    }
}
