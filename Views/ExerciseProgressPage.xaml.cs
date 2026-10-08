using w12.Services;
using w12.ViewModels;

namespace w12.Views
{
    public partial class ExerciseProgressPage : ContentPage
    {
        private readonly ExerciseProgressViewModel _viewModel;
        private readonly Database _dataBase;

        public ExerciseProgressPage(Database database)
        {
            this._dataBase = database;
            InitializeComponent();
            _viewModel =  new ExerciseProgressViewModel(this._dataBase);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InitializeAsync();
        }
    }
}