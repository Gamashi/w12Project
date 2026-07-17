using w12.Services;

namespace w12.Views;

public partial class ExerciseModelMagenimentPage : ContentPage
{
    private readonly Database _dataBase;
    public ExerciseModelMagenimentPage(Database database)
	{
        this._dataBase = database;
        InitializeComponent();
        BindingContext = new ViewModels.ExerciseModelMagenimentViewModel(this._dataBase);
    }
}