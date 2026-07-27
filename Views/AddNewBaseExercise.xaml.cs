using w12.Services;
using w12.ViewModels;

namespace w12.Views;

public partial class AddNewBaseExercise : ContentPage
{
	private readonly Database _dataBase;
    // A ViewModel vem injetada diretamente aqui!
    public AddNewBaseExercise(Database database, AddNewBaseExerciseViewModel viewModel)
    {
        InitializeComponent();
        _dataBase = database;
        BindingContext = viewModel; // Agora o Shell consegue gerenciar o lifecycle e passar os parâmetros!
    }
}