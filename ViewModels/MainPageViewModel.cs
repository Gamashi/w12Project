using CommunityToolkit.Mvvm.ComponentModel;
using w12.Services;
using w12.Models;
using CommunityToolkit.Mvvm.Input;
using w12.Views;
using CommunityToolkit.Mvvm.Messaging;
using w12.Messages;

namespace w12.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly Database _dataBase;
        [ObservableProperty]
        public ExecutionExercise execution = new ExecutionExercise();
        [ObservableProperty]
        public string userName = Preferences.Get("UserName", string.Empty);
        [ObservableProperty]
        public string dateTimeString = string.Empty;
        public MainPageViewModel(Database database)
        {
            this._dataBase = database;
            WeakReferenceMessenger.Default.Register<ExerciseAddedMessage>(this, (r, m) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Execution = m.Value;;
                });
            });
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

            if(executionExercises.Count == 0) 
            {
                return;
            }               
            Execution = executionExercises.Last();
            DateTimeString = Execution.ExecutionDate.ToString("dd/MM/yyyy");
        }
        [RelayCommand]
        async Task NavigateToAddExercise()
        {
            await Shell.Current.GoToAsync(nameof(AddNewExercisePage));
        }
        [RelayCommand]
        async Task NavigateToExerciseModelMagenimentPage()
        {
            await Shell.Current.GoToAsync(nameof(ExerciseModelMagenimentPage));
        }
    }
}
