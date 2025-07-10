using CommunityToolkit.Mvvm.ComponentModel;
using w12.Services;
using w12.Models;
using CommunityToolkit.Mvvm.Input;
using w12.Views;

namespace w12.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly Database _dataBase;
        private ExecutionExercise executinoExercice = new ExecutionExercise();
        public MainPageViewModel(Database database)
        {
            this._dataBase = database;
        }
        [RelayCommand]
        void SearchLastExercise()
        {
            //executinoExercice = _dataBase.
        }
        [RelayCommand]
        async void NavigateToAddExercise()
        {
            await Shell.Current.GoToAsync(nameof(AddNewExercisePage));
        }  
    }
}
