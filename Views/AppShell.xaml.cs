using w12.Views;

namespace w12
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddNewExercisePage), typeof(AddNewExercisePage));
        }
    }
}
