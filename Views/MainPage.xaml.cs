using System.Threading.Tasks;
using w12.Models;
using w12.Services;

namespace w12
{
    public partial class MainPage : ContentPage
    {
        private readonly Database _dataBase;

        public MainPage(Database database)
        {
            this._dataBase = database;
            InitializeComponent();
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            BaseExercise a = new BaseExercise();
            a = await _dataBase.GetItemAsync(1);
        }
    }
}
