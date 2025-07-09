using w12.ViewModels;

namespace w12.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterPageViewModel();
    }
}