using w12.Services;

namespace w12.Views;

public partial class AddNewExercisePage : ContentPage
{
	private readonly Services.Database _dataBase;
    public AddNewExercisePage(Database database)
	{
		this._dataBase = database;
        InitializeComponent();
		BindingContext = new ViewModels.AddNewExerciseViewModel(this._dataBase);
    }
}