using w12.Services;
using w12.Views;

namespace w12
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {

            bool isRegistered = Preferences.Get("IsRegistered", false);
            if (isRegistered)
            {
                return new Window(new AppShell());
            }
            return new Window(new RegisterPage());            
        }
    }
}