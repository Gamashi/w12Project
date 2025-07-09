using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using w12.Models;

namespace w12.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public User user = new User();

        [RelayCommand]
        void Register()
        {
            if (string.IsNullOrEmpty(User.Name) || string.IsNullOrEmpty(User.Email))
            {
                return;
            }
            Preferences.Set("IsRegistered", true);
            Application.Current.MainPage = new AppShell();
        }
    }
}
