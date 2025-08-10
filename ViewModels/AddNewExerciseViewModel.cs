using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using w12.Models;

namespace w12.ViewModels
{
    public partial class AddNewExerciseViewModel : ObservableObject
    {
        private readonly Services.Database _dataBase;
        private List<BaseExercise> _exercises = new List<BaseExercise>();
        [ObservableProperty]
        public ObservableCollection<BaseExercise> baseexercises = new ObservableCollection<BaseExercise>();
        [ObservableProperty]
        public ExecutionExercise exercise = new ExecutionExercise();
        public AddNewExerciseViewModel(Services.Database database)
        {
            this._dataBase = database;
            GetExercisesBase();
        }
        async void GetExercisesBase()
        {
            var exercises = await _dataBase.GetBaseExercisesAsync();
            if (exercises != null && exercises.Count > 0)
            {
                foreach (var exercise in exercises)
                {
                    Baseexercises.Add(exercise);
                }
            }
        }
        [RelayCommand]
        public async Task AddNewExercise()
        {
            if(Exercise.BaseExercise == null)
            {
                ShowToast("Selecione uma categoria para o seu exercício");
                return;
            }
            if(Exercise.RepetitionCount <= 0)
            {
                ShowToast("Número de repetições inválido");
                return;
            }
            if(Exercise.NumberOfSets <=0)
            {
                ShowToast("Número de séries inválido");
                return;
            }
            Exercise.ExecutionDate = DateTime.Now.Date;
            Exercise.BaseExerciseId = Exercise.BaseExercise.BaseExerciseId;
            var result =  await _dataBase.SaveExecutionExercise(Exercise); 
            if(result > 0)
            {
                ShowToast("Exercício salvo com sucesso!");
                Exercise = new ExecutionExercise();
            }
            else
            {
                ShowToast("Erro ao salvar o exercício.");
            }
        }
        private void ShowToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string text = message;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);
            toast.Show(cancellationTokenSource.Token);
        }
    }
}
