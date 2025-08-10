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
        [ObservableProperty]
        public ExecutionExercise execution = new ExecutionExercise();
        public MainPageViewModel(Database database)
        {
            this._dataBase = database;
            GetLast();
        }
        [RelayCommand]
        void SearchLastExercise()
        {

        }

        async void GetLast()
        {
            List<ExecutionExercise> executionExercises = new List<ExecutionExercise>();
            executionExercises = await _dataBase.GetExecutionExercisesListAsync();

            Execution = executionExercises.Last();
        }
        [RelayCommand]
        async Task NavigateToAddExercise()
        {
            await Shell.Current.GoToAsync(nameof(AddNewExercisePage));
        }
        [RelayCommand]
        async Task NavigateToAddNewBaseExercise()
        {
            await Shell.Current.GoToAsync(nameof(AddNewBaseExercise));
        }
    }
}
