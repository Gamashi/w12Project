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
            string[] names = user.Name.Trim().Split(' ');
            string firstName = names[0].ToUpper();
            Preferences.Set("UserName", firstName);
            Application.Current.MainPage = new AppShell();
        }
    }
}
