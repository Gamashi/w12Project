namespace w12.Views;

public partial class OneRepMaxCardView : ContentView
{
	public OneRepMaxCardView()
	{
		InitializeComponent();
	}

    private async void OnInfoTapped(object sender, TappedEventArgs e)
    {
        if (Application.Current?.Windows.FirstOrDefault()?.Page is Page currentPage)
        {
            await currentPage.DisplayAlert(
                "O que é 1RM?",
                "1RM (Uma Repetição Máxima) é a carga máxima teórica que você levantaria em uma única repetição.\n\nCalculamos isso automaticamente com base nas suas repetições e peso através da fórmula de Brzycki.",
                "Entendi"
            );
        }
    }
}