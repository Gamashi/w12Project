using w12.Services;

namespace w12.Views;

public partial class AddNewBaseExercise : ContentPage
{
	private readonly Database _dataBase;
    public AddNewBaseExercise(Database database)
	{
		this._dataBase = database;
        InitializeComponent();
		BindingContext = new ViewModels.AddNewBaseExerciseViewModel(this._dataBase);
    }
}