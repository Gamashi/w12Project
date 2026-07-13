using CommunityToolkit.Mvvm.Messaging.Messages;
using w12.Models;

namespace w12.Messages
{
    public class ExerciseAddedMessage : ValueChangedMessage<ExecutionExercise>
    {
        public ExerciseAddedMessage(ExecutionExercise exercise) : base(exercise)
        {
        }
    }
}