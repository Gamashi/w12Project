using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using w12.Helpers;
using w12.Messages;
using w12.Models;
using w12.Services;
using w12.Views;

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

        [ObservableProperty]
        private string _rmTitle = "RECORDE PESSOAL (1RM)";
        [ObservableProperty]
        private string _rmExerciseName = "Nenhum exercício registrado";
        [ObservableProperty]
        private double _estimated1RM;
        [ObservableProperty]
        private bool _has1RMData;

        public MainPageViewModel(Database database)
        {
            this._dataBase = database;
            WeakReferenceMessenger.Default.Register<ExerciseAddedMessage>(this, (r, m) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    //Execution = m.Value;
                    GetLast();
                });
            });
            GetLast();            
        }
        private void LoadOneRepMaxCard(List<ExecutionExercise> executions)
        {
            var summary = OneRepMaxCalculator.GetCardSummary(executions);

            Has1RMData = summary.HasData;
            RmTitle = summary.Title;
            RmExerciseName = summary.ExerciseName;
            Estimated1RM = summary.Estimated1RM;
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
            LoadOneRepMaxCard(executionExercises);
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
