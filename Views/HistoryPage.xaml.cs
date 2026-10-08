using w12.Services;
using w12.ViewModels;

namespace w12.Views
{
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryViewModel _viewModel;
        private readonly Database _dataBase;

        public HistoryPage(Database database)
        {
            this._dataBase = database;
            InitializeComponent();
            _viewModel = new HistoryViewModel(this._dataBase);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadHistoryAsync();
        }
    }
}