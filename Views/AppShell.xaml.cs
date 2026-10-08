using w12.Views;

namespace w12
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddNewExercisePage), typeof(AddNewExercisePage));
            Routing.RegisterRoute(nameof(AddNewBaseExercise), typeof(AddNewBaseExercise));
            Routing.RegisterRoute(nameof(ExerciseModelMagenimentPage), typeof(ExerciseModelMagenimentPage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(ExerciseProgressPage), typeof(ExerciseProgressPage));
        }
    }
}
