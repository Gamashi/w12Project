using CommunityToolkit.Mvvm.Messaging.Messages;
using w12.Models;

namespace w12.Messages 
{
    public class BaseExerciseAddedMessage : ValueChangedMessage<BaseExercise>
    {
        public BaseExerciseAddedMessage(BaseExercise exercise) : base(exercise)
        {
        }
    }
}
